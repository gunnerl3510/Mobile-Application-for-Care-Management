// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Accounts.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Business logic for working with <seealso cref="AccountModels.Account" /> objects
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BusinessLogic.Account
{
    using System;
    using System.Linq;
    using System.Security;
    using System.Security.Principal;
    using System.Web.Security;

    using BusinessLogic.Helpers;

    using Data.Repository;

    using Infrastructure.Logging;
    using Infrastructure.Security;

    using Ninject;

    using AccountModels = Infrastructure.Model.Account;

    /// <summary>
    /// Business logic for working with <seealso cref="AccountModels.Account"/> objects
    /// </summary>
    public class Accounts
    {
        #region private members

        /// <summary>
        /// The <c>IReadOnlyRepository</c> for the <c>Account</c> models
        /// </summary>
        private readonly IReadOnlyRepository<AccountModels.Account> accountReadRepository;

        /// <summary>
        /// The <c>IReadOnlyRepository</c> for the <c>Account</c> models
        /// </summary>
        private readonly IRepository<AccountModels.Account> accountRepository;

        /// <summary>
        /// The <c>ILogger</c> to use for logging messages
        /// </summary>
        private readonly ILogger<Accounts> logger;

        /// <summary>
        /// The object that implements the <c>IKernel</c> interface used for
        /// dependency injection
        /// </summary>
        private readonly IKernel kernel;

        #endregion

        #region public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Accounts"/> class.
        /// Interfaces are used for initialization to facilitate dependency injection.
        /// </summary>
        /// <param name="accountReadRepository">
        /// The <c>IReadOnlyRepository</c> to use
        /// for retrieving <c>Account</c> records from the repository
        /// </param>
        /// <param name="accountRepository">
        /// The <c>IRepository</c> to use
        /// for adding, deleting, and updating <c>Account</c> records in the 
        /// repository
        /// </param>
        /// <param name="logger">
        /// The <c>ILogger</c> to user to log messages
        /// </param>
        /// <param name="kernel">
        /// The <c>IKernel</c> to use for dependency injection
        /// </param>
        public Accounts(
            IReadOnlyRepository<AccountModels.Account> accountReadRepository,
            IRepository<AccountModels.Account> accountRepository,
            ILogger<Accounts> logger,
            IKernel kernel)
        {
            this.accountReadRepository = accountReadRepository;
            this.accountRepository = accountRepository;
            this.logger = logger;
            this.kernel = kernel;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Adds an account to the repository and creates a membership entry
        /// in the <c>IMembershipProvider</c> 
        /// </summary>
        /// <param name="account">The <c>Account</c> to add to the repository</param>
        /// <param name="password">The new account's password </param>
        /// <exception cref="ArgumentNullException">Thrown if the account parameter
        /// is null</exception>
        /// <exception cref="ArgumentNullException">Thrown if the account's Contacts
        /// property is null</exception>
        /// <exception cref="ArgumentException">Thrown if the password is null or
        /// empty string</exception>
        /// <exception cref="ArgumentException">Thrown if the account does not have
        /// exactly one contact that is designated as the primary and that contact
        /// does not have exactly one email address and one phone number that is 
        /// designated as primary</exception>
        /// <exception cref="SecurityException">Thrown if the user cannot properly be
        /// authenticated or the user is not authorized to add the address to the
        /// repository</exception>
        /// <exception cref="MembershipCreateUserException">The user was not created
        /// by the membership provider.</exception>
        public virtual void AddAccount(AccountModels.Account account, string password)
        {
            logger.EnterMethod("AddAccount");

            Invariant.IsNotNull(account, "account");
            Invariant.IsNotBlank(password, "password");

            try
            {
                var newUserAccount = Membership.CreateUser(account.Name, password, account.EmailAddress);

                // ReSharper disable PossibleNullReferenceException
                account.UserId = (Guid)newUserAccount.ProviderUserKey;
                // ReSharper restore PossibleNullReferenceException
                accountRepository.Add(account);
            }
            catch (MembershipCreateUserException exception)
            {
                var createErrorDescription = CreateMembershipStatusDescriptions.Status[exception.StatusCode];

                logger.LogExceptionWithMessage(exception, createErrorDescription);

                throw;
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("AddAccount");
        }

        /// <summary>
        /// Deletes an account from the membership store and removes the
        /// account from the repository
        /// </summary>
        /// <param name="account">The <c>Account</c> to delete from the repository</param>
        /// <param name="identity">The <c>IIdentity</c> of the user authorized to
        /// delete the <c>Account</c> from the repository</param>
        /// <exception cref="SecurityException">Thrown if the user cannot properly be
        /// authenticated or the user is not authorized to delete the account from
        /// the repository</exception>
        public void DeleteAccount(AccountModels.Account account, IIdentity identity)
        {
            logger.EnterMethod("DeleteAccount");

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, account.Id);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in DeleteAccount");
                throw;
            }

            try
            {
                accountRepository.Delete(account);

                Membership.DeleteUser(account.Name, true);
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("DeleteAccount");
        }

        /// <summary>
        /// Updates an account in the repository and the email addresss in the
        /// membership store
        /// </summary>
        /// <param name="account">The <c>Account</c> to update in the repository</param>
        /// <param name="identity">The <c>IIdentity</c> of the user authorized to
        /// update the <c>Account</c> in the repository</param>
        /// <exception cref="SecurityException">Thrown if the user cannot properly be
        /// authenticated or the user is not authorized to update the account in
        /// the repository</exception>
        public void UpdateAccount(AccountModels.Account account, IIdentity identity)
        {
            logger.EnterMethod("UpdateAccount");

            if (account == null)
            {
                throw new ArgumentNullException("account", "The account parameter cannot be null.");
            }

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, account.Id);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in UpdateAccount");
                throw;
            }

            try
            {
                var membershipUser = Membership.GetUser(account.Name, false);

                // ReSharper disable PossibleNullReferenceException
                membershipUser.Email = account.EmailAddress;
                // ReSharper restore PossibleNullReferenceException
                Membership.UpdateUser(membershipUser);

                accountRepository.Update(account);
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("UpdateAccount");
        }

        #region account retrieval

        /// <summary>
        /// Retrieves a list of accounts from the system.
        /// </summary>
        /// <param name="identity">
        /// The <seealso cref="IIdentity"/> of the user requesting the list of accounts.
        /// </param>
        /// <returns>
        /// An <seealso cref="IQueryable{T}"/> of <seealso cref="AccountModels.Account"/>.
        /// </returns>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not an administrator and therefore not authorized to retrieve the list of accounts.
        /// </exception>
        public IQueryable<AccountModels.Account> GetAccounts(IIdentity identity)
        {
            logger.EnterMethod("GetAccounts");

            Invariant.IsNotNull(identity, "identity");

            if (!Roles.IsUserInRole(identity.Name, "Admin"))
            {
                throw new SecurityException(
                    string.Format("The user {0} is not authorized to retrieve the list of accounts", identity.Name));
            }

            var accounts = accountReadRepository.All();

            logger.LeaveMethod("GetAccounts");

            return accounts;
        }

        /// <summary>
        /// Retrieves an <c>Account</c> from the repository using the id of
        /// the account
        /// </summary>
        /// <param name="accountId">The id of the account to retrieve</param>
        /// <param name="identity">The identity whose credentials are used to authenticate
        /// and authorize the action</param>
        /// <returns>The retrieved <c>Account</c> if it is found in the repository
        /// or null if it is not found</returns>
        public AccountModels.Account GetAccountById(int accountId, IIdentity identity)
        {
            logger.EnterMethod("GetAccountById");

            var requestedAccount = accountReadRepository.FindBy(account => account.Id.Equals(accountId));

            var user = Membership.GetUser(identity.Name, false);

            if (user == null || !requestedAccount.UserId.Equals(user.ProviderUserKey))
            {
                var securityException =
                    new SecurityException(
                        string.Format(
                            "The user {0} is not a system administrator and therefore cannot retrieve the list of accounts.",
                            identity.Name));

                logger.LogExceptionWithMessage(securityException, "SecurityException thrown in GetAccountById");

                throw securityException;
            }

            logger.LeaveMethod("GetAccountById");
            return requestedAccount;
        }

        /// <summary>
        /// Retrieves the account associated with the provided identity.
        /// </summary>
        /// <param name="identity">
        /// The identity to retrieve the account information for.
        /// </param>
        /// <returns>
        /// The account if it is found, null otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the identity parameter is null.
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the identity provided is not authenticated.
        /// </exception>
        public AccountModels.Account GetAccountByIdentity(IIdentity identity)
        {
            Invariant.IsNotNull(identity, "identity");

            if (!identity.IsAuthenticated)
            {
                throw new SecurityException("The identity provided has not been authenticated.");
            }

            var user = Membership.GetUser(identity.Name);
            return accountReadRepository.FindBy(account => account.UserId.Value.Equals((Guid)user.ProviderUserKey));
        }

        #endregion

        #endregion
    }
}
