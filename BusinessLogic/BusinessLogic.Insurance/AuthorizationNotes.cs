// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthorizationNotes.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Business logic for working with <seealso cref="InsuranceModels.AuthorizationNote" /> objects
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BusinessLogic.Insurance
{
    using System;
    using System.Linq;
    using System.Security;
    using System.Security.Principal;
    using System.Web.Security;

    using BusinessLogic.Helpers;

    using Data.Repository;

    using Infrastructure.Logging;

    using Ninject;

    using AccountModels = Infrastructure.Model.Account;
    using InsuranceModels = Infrastructure.Model.Insurance;

    /// <summary>
    /// Business logic for working with <seealso cref="InsuranceModels.AuthorizationNote"/> objects
    /// </summary>
    public class AuthorizationNotes
    {
        #region private members

        /// <summary>
        /// The <seealso cref="IReadOnlyRepository{T}"/> for the 
        /// <seealso cref="InsuranceModels.AuthorizationNote"/> models
        /// </summary>
        private readonly IReadOnlyRepository<InsuranceModels.AuthorizationNote> authorizationNoteReadOnlyRepository;

        /// <summary>
        /// The <seealso cref="IRepository{T}"/> for the 
        /// <seealso cref="InsuranceModels.AuthorizationNote"/> models
        /// </summary>
        private readonly IRepository<InsuranceModels.AuthorizationNote> authorizationNoteRepository;

        /// <summary>
        /// The <seealso cref="ILogger{T}"/> to use for logging messages
        /// </summary>
        private readonly ILogger<AuthorizationNotes> logger;

        /// <summary>
        /// The object that implements the <seealso cref="IKernel"/> interface used for
        /// dependency injection
        /// </summary>
        private readonly IKernel kernel;

        #endregion

        #region public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationNotes"/> class.
        /// Interfaces are used for initialization to facilitate dependency injection.
        /// </summary>
        /// <param name="authorizationNoteReadOnlyRepository">
        /// The <seealso cref="IReadOnlyRepository{T}"/> to use for retrieving 
        /// <seealso cref="InsuranceModels.AuthorizationNote"/> records from the repository
        /// </param>
        /// <param name="authorizationNoteRepository">
        /// The <seealso cref="IRepository{T}"/> to use for adding, deleting, and updating 
        /// <seealso cref="InsuranceModels.AuthorizationNote"/> records in the  repository
        /// </param>
        /// <param name="logger">
        /// The <seealso cref="ILogger{T}"/> to use to log messages
        /// </param>
        /// <param name="kernel">
        /// The <seealso cref="IKernel"/> to use for dependency injection
        /// </param>
        public AuthorizationNotes(
            IReadOnlyRepository<InsuranceModels.AuthorizationNote> authorizationNoteReadOnlyRepository,
            IRepository<InsuranceModels.AuthorizationNote> authorizationNoteRepository,
            ILogger<AuthorizationNotes> logger,
            IKernel kernel)
        {
            this.authorizationNoteReadOnlyRepository = authorizationNoteReadOnlyRepository;
            this.authorizationNoteRepository = authorizationNoteRepository;
            this.logger = logger;
            this.kernel = kernel;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Adds an <seealso cref="InsuranceModels.AuthorizationNote"/> to the repository
        /// </summary>
        /// <param name="authorizationNote">
        /// The <seealso cref="InsuranceModels.AuthorizationNote"/> to add.
        /// </param>
        /// <param name="identity">
        /// The identity of the user authorized to add the authorization note to the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="authorizationNote"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to add the 
        /// <seealso cref="InsuranceModels.AuthorizationNote"/> to the repository
        /// </exception>
        public void CreateAuthorizationNote(InsuranceModels.AuthorizationNote authorizationNote, IIdentity identity)
        {
            logger.EnterMethod("CreateAuthorizationNote");

            Invariant.IsNotNull(authorizationNote, "authorizationNote");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                var accountId =
                    kernel.Get<IReadOnlyRepository<InsuranceModels.AuthorizationRequest>>().FindBy(
                        request => request.Id.Equals(authorizationNote.AuthorizationRequestId)).AccountId;

                kernel.Get<Security>().AuthorizeAction(identity, accountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in CreateAuthorizationNote");
                throw;
            }

            try
            {
                this.authorizationNoteRepository.Add(authorizationNote);
            }
            catch (Exception exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("CreateAuthorizationNote");
        }

        /// <summary>
        /// Deletes an <seealso cref="InsuranceModels.AuthorizationNote"/> from the repository
        /// </summary>
        /// <param name="authorizationNote">
        /// The <seealso cref="InsuranceModels.AuthorizationNote"/> to delete from the repository
        /// </param>
        /// <param name="identity">
        /// The <c>IIdentity</c> of the user authorized to delete the 
        /// <seealso cref="InsuranceModels.AuthorizationNote"/> from the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="authorizationNote"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to delete the authorization note from the repository
        /// </exception>
        public void DeleteAuthorizationNote(InsuranceModels.AuthorizationNote authorizationNote, IIdentity identity)
        {
            logger.EnterMethod("DeleteAuthorizationNote");

            Invariant.IsNotNull(authorizationNote, "authorizationNote");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                var accountId =
                    kernel.Get<IReadOnlyRepository<InsuranceModels.AuthorizationRequest>>().FindBy(
                        request => request.Id.Equals(authorizationNote.AuthorizationRequestId)).AccountId;

                kernel.Get<Security>().AuthorizeAction(identity, accountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in DeleteAuthorizationNote");
                throw;
            }

            try
            {
                this.authorizationNoteRepository.Delete(authorizationNote);
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("DeleteAuthorizationNote");
        }

        /// <summary>
        /// Updates an <seealso cref="InsuranceModels.AuthorizationNote"/> in the repository
        /// </summary>
        /// <param name="authorizationNote">
        /// The <seealso cref="InsuranceModels.AuthorizationNote"/> to update in the repository
        /// </param>
        /// <param name="identity">
        /// The <c>IIdentity</c> of the user authorized to update the 
        /// <seealso cref="InsuranceModels.AuthorizationNote"/> in the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="authorizationNote"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to update the authorization note in
        /// the repository
        /// </exception>
        public void UpdateAuthorizationNote(InsuranceModels.AuthorizationNote authorizationNote, IIdentity identity)
        {
            logger.EnterMethod("UpdateAuthorizationNote");

            Invariant.IsNotNull(authorizationNote, "authorizationNote");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                var accountId =
                    kernel.Get<IReadOnlyRepository<InsuranceModels.AuthorizationRequest>>().FindBy(
                        request => request.Id.Equals(authorizationNote.AuthorizationRequestId)).AccountId;

                kernel.Get<Security>().AuthorizeAction(identity, accountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in UpdateAuthorizationNote");
                throw;
            }

            try
            {
                this.authorizationNoteRepository.Update(authorizationNote);
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("UpdateAuthorizationNote");
        }

        #region AuthorizationNote retrieval

        /// <summary>
        /// Retrives an <seealso cref="IQueryable{T}"/> of <seealso cref="InsuranceModels.AuthorizationNote"/> from
        /// the repository.
        /// </summary>
        /// <param name="identity">
        /// The identity of the user requesting the follow ups.
        /// </param>
        /// <returns>
        /// An <seealso cref="IQueryable{T}"/> of <seealso cref="InsuranceModels.AuthorizationNote"/>.
        /// </returns>
        public IQueryable<InsuranceModels.AuthorizationNote> GetAuthorizationNotes(IIdentity identity)
        {
            logger.EnterMethod("GetAuthorizationNotes");

            Invariant.IsNotNull(identity, "identity");

            IQueryable<InsuranceModels.AuthorizationNote> authorizationNotes;

            if (Roles.IsUserInRole(identity.Name, "Admin"))
            {
                authorizationNotes = authorizationNoteReadOnlyRepository.All();
            }
            else
            {
                var user = Membership.GetUser(identity.Name, false);
                var accountReadRepository = kernel.Get<IReadOnlyRepository<AccountModels.Account>>();
                var userAccount = accountReadRepository.FindBy(account => account.UserId.Value.Equals((Guid)user.ProviderUserKey));

                authorizationNotes = authorizationNoteReadOnlyRepository.FilterBy(note => note.AccountId.Equals(userAccount.Id));
            }

            logger.LeaveMethod("GetAuthorizationFollowUps");

            return authorizationNotes;
        }

        /// <summary>
        /// Retrieves an <seealso cref="InsuranceModels.AuthorizationNote"/> from the 
        /// repository using the id of the note
        /// </summary>
        /// <param name="noteId">
        /// The id of the <seealso cref="InsuranceModels.AuthorizationNote"/> to retrieve
        /// </param>
        /// <param name="identity">
        /// The identity whose credentials are used to authorize the action
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the <paramref name="noteId"/> parameter is less than 1
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to retrieve the authorization note in
        /// the repository
        /// </exception>
        /// <returns>
        /// The retrieved <seealso cref="InsuranceModels.AuthorizationNote"/> if it is 
        /// found in the repository or null if it is not found
        /// </returns>
        public InsuranceModels.AuthorizationNote GetAuthorizationNoteById(int noteId, IIdentity identity)
        {
            logger.EnterMethod("GetAuthorizationNoteById");

            Invariant.IsNotNull(identity, "identity");

            if (noteId < 1)
            {
                throw new ArgumentOutOfRangeException("noteId", noteId, "The noteId parameter must be greater than 0.");
            }

            var requestedNote = this.authorizationNoteReadOnlyRepository.FindBy(note => note.Id.Equals(noteId));

            try
            {
                var accountId =
                    kernel.Get<IReadOnlyRepository<InsuranceModels.AuthorizationRequest>>().FindBy(
                        request => request.Id.Equals(requestedNote.AuthorizationRequestId)).AccountId;

                kernel.Get<Security>().AuthorizeAction(identity, accountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in UpdateAuthorizationNote");
                throw;
            }

            logger.LeaveMethod("GetAuthorizationNoteById");
            return requestedNote;
        }

        /// <summary>
        /// Retrieves a collection of <seealso cref="InsuranceModels.AuthorizationNote"/>
        /// that have an authorization request id that matches the <paramref name="requestId"/> passed in
        /// </summary>
        /// <param name="requestId">
        /// The id of the authorization request to retrieve the <seealso cref="InsuranceModels.AuthorizationNote"/> for
        /// </param>
        /// <param name="identity">
        /// The <seealso cref="IIdentity"/> that contains the identity of the user that is authorized
        /// to retrieve the records from the repository
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the <paramref name="requestId"/> parameter is less than 1
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to retrieve the authorization notes for the authorization request
        ///  specified from the repository
        /// </exception>
        /// <returns>
        /// An <seealso cref="IQueryable{T}"/> collection of <seealso cref="InsuranceModels.AuthorizationNote"/>
        /// that belong to authorization request identified by <paramref name="requestId"/>
        /// </returns>
        public IQueryable<InsuranceModels.AuthorizationNote> GetAuthorizationNotesByAuthorizationRequest(int requestId, IIdentity identity)
        {
            logger.EnterMethod("GetAuthorizationNotesByAuthorizationRequest");

            Invariant.IsNotNull(identity, "identity");

            if (requestId < 1)
            {
                throw new ArgumentOutOfRangeException("requestId", requestId, "The accountId parameter must be greater than 0.");
            }

            try
            {
                var accountId =
                    kernel.Get<IReadOnlyRepository<InsuranceModels.AuthorizationRequest>>().FindBy(
                        request => request.Id.Equals(requestId)).AccountId;

                kernel.Get<Security>().AuthorizeAction(identity, accountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in GetAuthorizationNotesByAuthorizationRequest");
                throw;
            }

            var notes = authorizationNoteReadOnlyRepository.FilterBy(request => request.AuthorizationRequestId.Equals(requestId));

            logger.LeaveMethod("GetAuthorizationNotesByAuthorizationRequest");

            return notes;
        }

        /// <summary>
        /// Retrieves a collection of <seealso cref="InsuranceModels.AuthorizationNote"/>
        /// that have an account id that matches the <paramref name="accountId"/> passed in
        /// </summary>
        /// <param name="accountId">
        /// The id of the account to retrieve the <seealso cref="InsuranceModels.AuthorizationNote"/> for
        /// </param>
        /// <param name="identity">
        /// The <seealso cref="IIdentity"/> that contains the identity of the user that is authorized
        /// to retrieve the records from the repository
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the <paramref name="accountId"/> parameter is less than 1
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to retrieve the authorization requests for the account specified
        /// from the repository
        /// </exception>
        /// <returns>
        /// An <seealso cref="IQueryable{T}"/> collection of <seealso cref="InsuranceModels.AuthorizationNote"/>
        /// that belong to account identified by <paramref name="accountId"/>
        /// </returns>
        public IQueryable<InsuranceModels.AuthorizationNote> GetAuthorizationNotesByAccount(int accountId, IIdentity identity)
        {
            logger.EnterMethod("GetAuthorizationNotesByAccount");

            Invariant.IsNotNull(identity, "identity");

            if (accountId < 1)
            {
                throw new ArgumentOutOfRangeException("accountId", accountId, "The accountId parameter must be greater than 0.");
            }

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, accountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in GetAuthorizationNotesByAccount");
                throw;
            }

            var requestIds =
                kernel.Get<IReadOnlyRepository<InsuranceModels.AuthorizationRequest>>().FilterBy(
                    request => request.AccountId.Equals(accountId)).Select(request => request.Id).ToList();

            var notes =
                authorizationNoteReadOnlyRepository.FilterBy(note => requestIds.Contains(note.AuthorizationRequestId));

            logger.LeaveMethod("GetAuthorizationNotesByAccount");

            return notes;
        }

        #endregion

        #endregion
    }
}
