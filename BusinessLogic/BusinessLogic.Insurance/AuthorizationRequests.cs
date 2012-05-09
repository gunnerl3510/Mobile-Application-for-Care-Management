// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthorizationRequests.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Business logic for working with AuthorizationRequest objects
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
    /// Business logic for working with <seealso cref="InsuranceModels.AuthorizationRequest"/> objects
    /// </summary>
    public class AuthorizationRequests
    {
        #region private members

        /// <summary>
        /// The <seealso cref="IReadOnlyRepository{T}"/> for the 
        /// <seealso cref="InsuranceModels.AuthorizationRequest"/> models
        /// </summary>
        private readonly IReadOnlyRepository<InsuranceModels.AuthorizationRequest> authorizationRequestReadOnlyRepository;

        /// <summary>
        /// The <seealso cref="IRepository{T}"/> for the 
        /// <seealso cref="InsuranceModels.AuthorizationRequest"/> models
        /// </summary>
        private readonly IRepository<InsuranceModels.AuthorizationRequest> authorizationRequestRepository;

        /// <summary>
        /// The <seealso cref="ILogger{T}"/> to use for logging messages
        /// </summary>
        private readonly ILogger<AuthorizationRequests> logger;

        /// <summary>
        /// The object that implements the <seealso cref="IKernel"/> interface used for
        /// dependency injection
        /// </summary>
        private readonly IKernel kernel;

        #endregion

        #region public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationRequests"/> class.
        /// Interfaces are used for initialization to facilitate dependency injection.
        /// </summary>
        /// <param name="authorizationRequestReadOnlyRepository">
        /// The <seealso cref="IReadOnlyRepository{T}"/> to use for retrieving 
        /// <seealso cref="InsuranceModels.AuthorizationRequest"/> records from the repository
        /// </param>
        /// <param name="authorizationRequestRepository">
        /// The <seealso cref="IRepository{T}"/> to use for adding, deleting, and updating 
        /// <seealso cref="InsuranceModels.AuthorizationRequest"/> records in the  repository
        /// </param>
        /// <param name="logger">
        /// The <seealso cref="ILogger{T}"/> to use to log messages
        /// </param>
        /// <param name="kernel">
        /// The <seealso cref="IKernel"/> to use for dependency injection
        /// </param>
        public AuthorizationRequests(
            IReadOnlyRepository<InsuranceModels.AuthorizationRequest> authorizationRequestReadOnlyRepository,
            IRepository<InsuranceModels.AuthorizationRequest> authorizationRequestRepository,
            ILogger<AuthorizationRequests> logger,
            IKernel kernel)
        {
            this.authorizationRequestReadOnlyRepository = authorizationRequestReadOnlyRepository;
            this.authorizationRequestRepository = authorizationRequestRepository;
            this.logger = logger;
            this.kernel = kernel;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Adds an <seealso cref="InsuranceModels.AuthorizationRequest"/> to the repository
        /// </summary>
        /// <param name="authorizationRequest">
        /// The <seealso cref="InsuranceModels.AuthorizationRequest"/> to add.
        /// </param>
        /// <param name="identity">
        /// The identity of the user authorized to add the authorization request to the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="authorizationRequest"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to add the 
        /// <seealso cref="InsuranceModels.AuthorizationRequest"/> to the repository
        /// </exception>
        public void CreateAuthorizationRequest(InsuranceModels.AuthorizationRequest authorizationRequest, IIdentity identity)
        {
            logger.EnterMethod("CreateAuthorizationRequest");

            Invariant.IsNotNull(authorizationRequest, "authorizationRequest");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, authorizationRequest.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in CreateAuthorizationRequest");
                throw;
            }

            try
            {
                authorizationRequestRepository.Add(authorizationRequest);
            }
            catch (Exception exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("CreateAuthorizationRequest");
        }

        /// <summary>
        /// Deletes an <seealso cref="InsuranceModels.AuthorizationRequest"/> from the repository
        /// </summary>
        /// <param name="authorizationRequest">
        /// The <seealso cref="InsuranceModels.AuthorizationRequest"/> to delete from the repository
        /// </param>
        /// <param name="identity">
        /// The <c>IIdentity</c> of the user authorized to delete the 
        /// <seealso cref="InsuranceModels.AuthorizationRequest"/> from the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="authorizationRequest"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to delete the authorization request from the repository
        /// </exception>
        public void DeleteAuthorizationRequest(InsuranceModels.AuthorizationRequest authorizationRequest, IIdentity identity)
        {
            logger.EnterMethod("DeleteAuthorizationRequest");

            Invariant.IsNotNull(authorizationRequest, "authorizationRequest");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, authorizationRequest.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in DeleteAuthorizationRequest");
                throw;
            }

            try
            {
                authorizationRequestRepository.Delete(authorizationRequest);
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("DeleteAuthorizationRequest");
        }

        /// <summary>
        /// Updates an <seealso cref="InsuranceModels.AuthorizationRequest"/> in the repository
        /// </summary>
        /// <param name="authorizationRequest">
        /// The <seealso cref="InsuranceModels.AuthorizationRequest"/> to update in the repository
        /// </param>
        /// <param name="identity">
        /// The <c>IIdentity</c> of the user authorized to update the 
        /// <seealso cref="InsuranceModels.AuthorizationRequest"/> in the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="authorizationRequest"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to update the authorization request in
        /// the repository
        /// </exception>
        public void UpdateAuthorizationRequest(InsuranceModels.AuthorizationRequest authorizationRequest, IIdentity identity)
        {
            logger.EnterMethod("UpdateAuthorizationRequest");

            Invariant.IsNotNull(authorizationRequest, "authorizationRequest");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, authorizationRequest.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in UpdateAuthorizationRequest");
                throw;
            }

            try
            {
                authorizationRequestRepository.Update(authorizationRequest);
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("UpdateAuthorizationRequest");
        }

        #region AuthorizationRequest retrieval

        /// <summary>
        /// Retrives an <seealso cref="IQueryable{T}"/> of <seealso cref="InsuranceModels.AuthorizationRequest"/> from
        /// the repository.
        /// </summary>
        /// <param name="identity">
        /// The identity of the user requesting the authorizations.
        /// </param>
        /// <returns>
        /// An <seealso cref="IQueryable{T}"/> of <seealso cref="InsuranceModels.AuthorizationRequest"/>.
        /// </returns>
        public IQueryable<InsuranceModels.AuthorizationRequest> GetAuthorizationRequests(IIdentity identity)
        {
            logger.EnterMethod("GetAuthorizationRequests");

            Invariant.IsNotNull(identity, "identity");

            IQueryable<InsuranceModels.AuthorizationRequest> authorizationRequests;

            if (Roles.IsUserInRole(identity.Name, "Admin"))
            {
                authorizationRequests = authorizationRequestReadOnlyRepository.All();
            }
            else
            {
                var user = Membership.GetUser(identity.Name, false);
                var accountReadRepository = kernel.Get<IReadOnlyRepository<AccountModels.Account>>();
                var userAccount = accountReadRepository.FindBy(account => account.UserId.Value.Equals((Guid)user.ProviderUserKey));

                authorizationRequests = authorizationRequestReadOnlyRepository.FilterBy(insurer => insurer.AccountId.Equals(userAccount.Id));
            }

            logger.LeaveMethod("GetAuthorizationRequests");

            return authorizationRequests;
        }

        /// <summary>
        /// Retrieves an <seealso cref="InsuranceModels.AuthorizationRequest"/> from the 
        /// repository using the id of the request
        /// </summary>
        /// <param name="requestId">
        /// The id of the <seealso cref="InsuranceModels.AuthorizationRequest"/> to retrieve
        /// </param>
        /// <param name="identity">
        /// The identity whose credentials are used to authorize the action
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the <paramref name="requestId"/> parameter is less than 1
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to retrieve the authorizationRequest in
        /// the repository
        /// </exception>
        /// <returns>
        /// The retrieved <seealso cref="InsuranceModels.AuthorizationRequest"/> if it is 
        /// found in the repository or null if it is not found
        /// </returns>
        public InsuranceModels.AuthorizationRequest GetAuthorizationRequestById(int requestId, IIdentity identity)
        {
            logger.EnterMethod("GetAuthorizationRequestById");

            Invariant.IsNotNull(identity, "identity");

            if (requestId < 1)
            {
                throw new ArgumentOutOfRangeException("requestId", requestId, "The requestId parameter must be greater than 0.");
            }

            var requestedRequest = authorizationRequestReadOnlyRepository.FindBy(request => request.Id.Equals(requestId));

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, requestedRequest.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in UpdateAuthorizationRequest");
                throw;
            }

            logger.LeaveMethod("GetAuthorizationRequestById");
            return requestedRequest;
        }

        /// <summary>
        /// Retrieves a collection of <seealso cref="InsuranceModels.AuthorizationRequest"/>
        /// that have an insurer id that matches the <paramref name="insurerId"/> passed in
        /// </summary>
        /// <param name="insurerId">
        /// The id of the insurer to retrieve the <seealso cref="InsuranceModels.AuthorizationRequest"/> for
        /// </param>
        /// <param name="identity">
        /// The <seealso cref="IIdentity"/> that contains the identity of the user that is authorized
        /// to retrieve the records from the repository
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the <paramref name="insurerId"/> parameter is less than 1
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to retrieve the authorization requests for the insurer specified
        /// from the repository
        /// </exception>
        /// <returns>
        /// An <seealso cref="IQueryable{T}"/> collection of <seealso cref="InsuranceModels.AuthorizationRequest"/>
        /// that belong to insurer identified by <paramref name="insurerId"/>
        /// </returns>
        public IQueryable<InsuranceModels.AuthorizationRequest> GetAuthorizationRequestsByInsurer(int insurerId, IIdentity identity)
        {
            logger.EnterMethod("GetAuthorizationRequestsByInsurer");

            Invariant.IsNotNull(identity, "identity");

            if (insurerId < 1)
            {
                throw new ArgumentOutOfRangeException("insurerId", insurerId, "The accountId parameter must be greater than 0.");
            }

            try
            {
                var insurerAccountId =
                    kernel.Get<IReadOnlyRepository<InsuranceModels.Insurer>>().FindBy(
                        insurer => insurer.Id.Equals(insurerId)).AccountId;    
                kernel.Get<Security>().AuthorizeAction(identity, insurerAccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in GetAuthorizationRequestsByInsurer");
                throw;
            }

            var insurers = authorizationRequestReadOnlyRepository.FilterBy(request => request.InsurerId.Equals(insurerId));

            logger.LeaveMethod("GetAuthorizationRequestsByInsurer");

            return insurers;
        }

        /// <summary>
        /// Retrieves a collection of <seealso cref="InsuranceModels.AuthorizationRequest"/>
        /// that have an account id that matches the <paramref name="accountId"/> passed in
        /// </summary>
        /// <param name="accountId">
        /// The id of the account to retrieve the <seealso cref="InsuranceModels.AuthorizationRequest"/> for
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
        /// An <seealso cref="IQueryable{T}"/> collection of <seealso cref="InsuranceModels.AuthorizationRequest"/>
        /// that belong to account identified by <paramref name="accountId"/>
        /// </returns>
        public IQueryable<InsuranceModels.AuthorizationRequest> GetAuthorizationRequestsByAccount(int accountId, IIdentity identity)
        {
            logger.EnterMethod("GetAuthorizationRequestsByAccount");

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
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in GetAuthorizationRequestsByAccount");
                throw;
            }

            var requests = authorizationRequestReadOnlyRepository.FilterBy(request => request.AccountId.Equals(accountId));

            logger.LeaveMethod("GetAuthorizationRequestsByAccount");

            return requests;
        }

        #endregion

        #endregion
    }
}
