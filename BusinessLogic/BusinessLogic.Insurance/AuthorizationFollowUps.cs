// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthorizationFollowUps.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Business logic for working with <seealso cref="InsuranceModels.AuthorizationFollowUp" /> objects
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BusinessLogic.Insurance
{
    using System;
    using System.Linq;
    using System.Security;
    using System.Security.Principal;

    using BusinessLogic.Helpers;

    using Data.Repository;

    using Infrastructure.Logging;

    using Ninject;

    using InsuranceModels = Infrastructure.Model.Insurance;

    /// <summary>
    /// Business logic for working with <seealso cref="InsuranceModels.AuthorizationFollowUp"/> objects
    /// </summary>
    public class AuthorizationFollowUps
    {
        #region private members

        /// <summary>
        /// The <seealso cref="IReadOnlyRepository{T}"/> for the 
        /// <seealso cref="InsuranceModels.AuthorizationFollowUp"/> models
        /// </summary>
        private readonly IReadOnlyRepository<InsuranceModels.AuthorizationFollowUp> authorizationFollowUpReadOnlyRepository;

        /// <summary>
        /// The <seealso cref="IRepository{T}"/> for the 
        /// <seealso cref="InsuranceModels.AuthorizationFollowUp"/> models
        /// </summary>
        private readonly IRepository<InsuranceModels.AuthorizationFollowUp> authorizationFollowUpRepository;

        /// <summary>
        /// The <seealso cref="ILogger{T}"/> to use for logging messages
        /// </summary>
        private readonly ILogger<AuthorizationFollowUps> logger;

        /// <summary>
        /// The object that implements the <seealso cref="IKernel"/> interface used for
        /// dependency injection
        /// </summary>
        private readonly IKernel kernel;

        #endregion

        #region public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationFollowUps"/> class.
        /// Interfaces are used for initialization to facilitate dependency injection.
        /// </summary>
        /// <param name="authorizationFollowUpReadOnlyRepository">
        /// The <seealso cref="IReadOnlyRepository{T}"/> to use for retrieving 
        /// <seealso cref="InsuranceModels.AuthorizationFollowUp"/> records from the repository
        /// </param>
        /// <param name="authorizationFollowUpRepository">
        /// The <seealso cref="IRepository{T}"/> to use for adding, deleting, and updating 
        /// <seealso cref="InsuranceModels.AuthorizationFollowUp"/> records in the  repository
        /// </param>
        /// <param name="logger">
        /// The <seealso cref="ILogger{T}"/> to use to log messages
        /// </param>
        /// <param name="kernel">
        /// The <seealso cref="IKernel"/> to use for dependency injection
        /// </param>
        public AuthorizationFollowUps(
            IReadOnlyRepository<InsuranceModels.AuthorizationFollowUp> authorizationFollowUpReadOnlyRepository,
            IRepository<InsuranceModels.AuthorizationFollowUp> authorizationFollowUpRepository,
            ILogger<AuthorizationFollowUps> logger,
            IKernel kernel)
        {
            this.authorizationFollowUpReadOnlyRepository = authorizationFollowUpReadOnlyRepository;
            this.authorizationFollowUpRepository = authorizationFollowUpRepository;
            this.logger = logger;
            this.kernel = kernel;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Adds an <seealso cref="InsuranceModels.AuthorizationFollowUp"/> to the repository
        /// </summary>
        /// <param name="authorizationFollowUp">
        /// The <seealso cref="InsuranceModels.AuthorizationFollowUp"/> to add.
        /// </param>
        /// <param name="identity">
        /// The identity of the user authorized to add the authorization follow up to the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="authorizationFollowUp"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to add the 
        /// <seealso cref="InsuranceModels.AuthorizationFollowUp"/> to the repository
        /// </exception>
        public void CreateAuthorizationFollowUp(InsuranceModels.AuthorizationFollowUp authorizationFollowUp, IIdentity identity)
        {
            logger.EnterMethod("CreateAuthorizationFollowUp");

            Invariant.IsNotNull(authorizationFollowUp, "authorizationFollowUp");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                var accountId =
                    kernel.Get<IReadOnlyRepository<InsuranceModels.AuthorizationRequest>>().FindBy(
                        request => request.Id.Equals(authorizationFollowUp.AuthorizationRequestId)).AccountId;

                kernel.Get<Security>().AuthorizeAction(identity, accountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in CreateAuthorizationFollowUp");
                throw;
            }

            try
            {
                authorizationFollowUpRepository.Add(authorizationFollowUp);
            }
            catch (Exception exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("CreateAuthorizationFollowUp");
        }

        /// <summary>
        /// Deletes an <seealso cref="InsuranceModels.AuthorizationFollowUp"/> from the repository
        /// </summary>
        /// <param name="authorizationFollowUp">
        /// The <seealso cref="InsuranceModels.AuthorizationFollowUp"/> to delete from the repository
        /// </param>
        /// <param name="identity">
        /// The <c>IIdentity</c> of the user authorized to delete the 
        /// <seealso cref="InsuranceModels.AuthorizationFollowUp"/> from the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="authorizationFollowUp"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to delete the authorization follow up from the repository
        /// </exception>
        public void DeleteAuthorizationFollowUp(InsuranceModels.AuthorizationFollowUp authorizationFollowUp, IIdentity identity)
        {
            logger.EnterMethod("DeleteAuthorizationFollowUp");

            Invariant.IsNotNull(authorizationFollowUp, "authorizationFollowUp");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                var accountId =
                    kernel.Get<IReadOnlyRepository<InsuranceModels.AuthorizationRequest>>().FindBy(
                        request => request.Id.Equals(authorizationFollowUp.AuthorizationRequestId)).AccountId;

                kernel.Get<Security>().AuthorizeAction(identity, accountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in DeleteAuthorizationFollowUp");
                throw;
            }

            try
            {
                authorizationFollowUpRepository.Delete(authorizationFollowUp);
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("DeleteAuthorizationFollowUp");
        }

        /// <summary>
        /// Updates an <seealso cref="InsuranceModels.AuthorizationFollowUp"/> in the repository
        /// </summary>
        /// <param name="authorizationFollowUp">
        /// The <seealso cref="InsuranceModels.AuthorizationFollowUp"/> to update in the repository
        /// </param>
        /// <param name="identity">
        /// The <c>IIdentity</c> of the user authorized to update the 
        /// <seealso cref="InsuranceModels.AuthorizationFollowUp"/> in the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="authorizationFollowUp"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to update the authorization follow up in
        /// the repository
        /// </exception>
        public void UpdateAuthorizationFollowUp(InsuranceModels.AuthorizationFollowUp authorizationFollowUp, IIdentity identity)
        {
            logger.EnterMethod("UpdateAuthorizationFollowUp");

            Invariant.IsNotNull(authorizationFollowUp, "authorizationFollowUp");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                var accountId =
                    kernel.Get<IReadOnlyRepository<InsuranceModels.AuthorizationRequest>>().FindBy(
                        request => request.Id.Equals(authorizationFollowUp.AuthorizationRequestId)).AccountId;

                kernel.Get<Security>().AuthorizeAction(identity, accountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in UpdateAuthorizationFollowUp");
                throw;
            }

            try
            {
                authorizationFollowUpRepository.Update(authorizationFollowUp);
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("UpdateAuthorizationFollowUp");
        }

        #region AuthorizationFollowUp retrieval

        /// <summary>
        /// Retrieves an <seealso cref="InsuranceModels.AuthorizationFollowUp"/> from the 
        /// repository using the id of the request
        /// </summary>
        /// <param name="followUpId">
        /// The id of the <seealso cref="InsuranceModels.AuthorizationFollowUp"/> to retrieve
        /// </param>
        /// <param name="identity">
        /// The identity whose credentials are used to authorize the action
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the <paramref name="followUpId"/> parameter is less than 1
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to retrieve the 
        /// <seealso cref="InsuranceModels.AuthorizationFollowUp"/> in the repository
        /// </exception>
        /// <returns>
        /// The retrieved <seealso cref="InsuranceModels.AuthorizationFollowUp"/> if it is 
        /// found in the repository or null if it is not found
        /// </returns>
        public InsuranceModels.AuthorizationFollowUp GetAuthorizationFollowUpById(int followUpId, IIdentity identity)
        {
            logger.EnterMethod("GetAuthorizationFollowUpById");

            Invariant.IsNotNull(identity, "identity");

            if (followUpId < 1)
            {
                throw new ArgumentOutOfRangeException("followUpId", followUpId, "The followUpId parameter must be greater than 0.");
            }

            var requestedFollowUp = authorizationFollowUpReadOnlyRepository.FindBy(request => request.Id.Equals(followUpId));

            try
            {
                var accountId =
                    kernel.Get<IReadOnlyRepository<InsuranceModels.AuthorizationRequest>>().FindBy(
                        request => request.Id.Equals(requestedFollowUp.AuthorizationRequestId)).AccountId;

                kernel.Get<Security>().AuthorizeAction(identity, accountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in UpdateAuthorizationFollowUp");
                throw;
            }

            logger.LeaveMethod("GetAuthorizationFollowUpById");
            return requestedFollowUp;
        }

        /// <summary>
        /// Retrieves a collection of <seealso cref="InsuranceModels.AuthorizationFollowUp"/>
        /// that have an authorization request id that matches the <paramref name="authorizationRequestId"/> passed in
        /// </summary>
        /// <param name="authorizationRequestId">
        /// The id of the authorization request to retrieve the <seealso cref="InsuranceModels.AuthorizationFollowUp"/> for
        /// </param>
        /// <param name="identity">
        /// The <seealso cref="IIdentity"/> that contains the identity of the user that is authorized
        /// to retrieve the records from the repository
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the <paramref name="authorizationRequestId"/> parameter is less than 1
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to retrieve the authorization follow ups for the authorization request specified
        /// from the repository
        /// </exception>
        /// <returns>
        /// An <seealso cref="IQueryable{T}"/> collection of <seealso cref="InsuranceModels.AuthorizationFollowUp"/>
        /// that belong to the authorization request identified by <paramref name="authorizationRequestId"/>
        /// </returns>
        public IQueryable<InsuranceModels.AuthorizationFollowUp> GetAuthorizationFollowUpsByAuthorizationRequest(int authorizationRequestId, IIdentity identity)
        {
            logger.EnterMethod("GetAuthorizationFollowUpsByAuthorizationRequest");

            Invariant.IsNotNull(identity, "identity");

            if (authorizationRequestId < 1)
            {
                throw new ArgumentOutOfRangeException("authorizationRequestId", authorizationRequestId, "The authorizationRequestId parameter must be greater than 0.");
            }

            try
            {
                var accountId =
                    kernel.Get<IReadOnlyRepository<InsuranceModels.AuthorizationRequest>>().FindBy(
                        authorizationRequest => authorizationRequest.Id.Equals(authorizationRequestId)).AccountId;    
                kernel.Get<Security>().AuthorizeAction(identity, accountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in GetAuthorizationFollowUpsByAuthorizationRequest");
                throw;
            }

            var authorizationFollowUps =
                authorizationFollowUpReadOnlyRepository.FilterBy(
                    request => request.AuthorizationRequestId.Equals(authorizationRequestId));

            logger.LeaveMethod("GetAuthorizationFollowUpsByAuthorizationRequest");

            return authorizationFollowUps;
        }

        /// <summary>
        /// Retrieves a collection of <seealso cref="InsuranceModels.AuthorizationFollowUp"/>
        /// for the account identified by the <paramref name="accountId"/> passed in
        /// </summary>
        /// <param name="accountId">
        /// The id of the account to retrieve the <seealso cref="InsuranceModels.AuthorizationFollowUp"/> for
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
        /// Thrown if the user is not authorized to retrieve the authorization follow ups for the account specified
        /// from the repository
        /// </exception>
        /// <returns>
        /// An <seealso cref="IQueryable{T}"/> collection of <seealso cref="InsuranceModels.AuthorizationFollowUp"/>
        /// that belong to the account identified by <paramref name="accountId"/>
        /// </returns>
        public IQueryable<InsuranceModels.AuthorizationFollowUp> GetAuthorizationFollowUpsByAccount(int accountId, IIdentity identity)
        {
            logger.EnterMethod("GetAuthorizationFollowUpsByAccount");

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
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in GetAuthorizationFollowUpsByAccount");
                throw;
            }

            var authorizationids =
                kernel.Get<IReadOnlyRepository<InsuranceModels.AuthorizationRequest>>().FilterBy(
                    request => request.AccountId.Equals(accountId)).Select(request => request.Id).ToList();

            var requests =
                authorizationFollowUpReadOnlyRepository.FilterBy(
                    request => authorizationids.Contains(request.AuthorizationRequestId));

            logger.LeaveMethod("GetAuthorizationFollowUpsByAccount");

            return requests;
        }

        #endregion

        #endregion
    }
}
