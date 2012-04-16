// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Security.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Helper class for security cross-cutting concerns
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BusinessLogic.Helpers
{
    using System;
    using System.Security;
    using System.Security.Principal;
    using System.Web.Security;

    using Data.Repository;

    using Infrastructure.Model.Account;

    /// <summary>
    /// Helper class for security cross-cutting concerns
    /// </summary>
    public class Security
    {
        #region private members

        /// <summary>
        /// The repository to use to retrieve account data
        /// </summary>
        private readonly IReadOnlyRepository<Account> accountReadRepository;

        #endregion

        #region public static constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Security"/> class.
        /// </summary>
        /// <param name="accountReadRepository">
        /// The repository to use to retrieve account data
        /// </param>
        public Security(IReadOnlyRepository<Account> accountReadRepository)
        {
            this.accountReadRepository = accountReadRepository;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Helper method to ensure that the <seealso cref="IIdentity"/> can authenticate against
        /// the <seealso cref="Membership"/> and that the <seealso cref="IIdentity"/> is also
        /// authorized to make the requested changes against the account retrieved
        /// from the accountId
        /// </summary>
        /// <param name="identity">
        /// The <c>IIdentity</c> of the user authorized to delete the <c>EmailAddress</c> 
        /// from the repository
        /// </param>
        /// <param name="accountId">
        /// The id of the <seealso cref="Account"/> to use for authorization
        /// </param>
        /// <exception cref="SecurityException">
        /// Thrown if the <seealso cref="IIdentity"/> cannot be authenticated or is not authorized 
        /// to access the records requested
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the <seealso cref="Account"/> with the provided id cannot be retrieved from the 
        /// repository
        /// </exception>
        public virtual void AuthorizeAction(IIdentity identity, int accountId)
        {
            var user = Membership.GetUser(identity.Name, false);

            if (user == null)
            {
                throw new SecurityException(
                    string.Format(
                        "The user {0} is not properly authenticated against the membership provider.", identity.Name));
            }

            var ownerAccount = accountReadRepository.FindBy(account => account.Id.Equals(accountId));

            if (ownerAccount == null)
            {
                throw new ArgumentException(
                    string.Format("The account with id {0} cannot be found", accountId), "accountId");
            }

            // ReSharper disable PossibleNullReferenceException
            if (!ownerAccount.UserId.Equals((Guid)user.ProviderUserKey))
            // ReSharper restore PossibleNullReferenceException)
            {
                throw new SecurityException(
                    string.Format(
                        "The user {0} is not authorized to access the method called for the account owned by {1}",
                        identity.Name,
                        ownerAccount.Name));
            }
        }

        #endregion
    }
}
