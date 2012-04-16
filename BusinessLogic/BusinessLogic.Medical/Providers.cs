// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Providers.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Business logic for working with <seealso cref="MedicalModels.Provider" /> objects
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BusinessLogic.Medical
{
    using System;
    using System.Linq;
    using System.Security;
    using System.Security.Principal;

    using BusinessLogic.Helpers;

    using Data.Repository;

    using Infrastructure.Logging;

    using Ninject;

    using MedicalModels = Infrastructure.Model.Medical;

    /// <summary>
    /// Business logic for working with <seealso cref="MedicalModels.Provider"/> objects
    /// </summary>
    public class Providers
    {
        #region private members

        /// <summary>
        /// The <seealso cref="IReadOnlyRepository{T}"/> for the 
        /// <seealso cref="MedicalModels.Provider"/> models
        /// </summary>
        private readonly IReadOnlyRepository<MedicalModels.Provider> providerReadOnlyRepository;

        /// <summary>
        /// The <seealso cref="IRepository{T}"/> for the 
        /// <seealso cref="MedicalModels.Provider"/> models
        /// </summary>
        private readonly IRepository<MedicalModels.Provider> providerRequestRepository;

        /// <summary>
        /// The <seealso cref="ILogger{T}"/> to use for logging messages
        /// </summary>
        private readonly ILogger<Providers> logger;

        /// <summary>
        /// The object that implements the <seealso cref="IKernel"/> interface used for
        /// dependency injection
        /// </summary>
        private readonly IKernel kernel;

        #endregion

        #region public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Providers"/> class.
        /// Interfaces are used for initialization to facilitate dependency injection.
        /// </summary>
        /// <param name="providerReadOnlyRepository">
        /// The <seealso cref="IReadOnlyRepository{T}"/> to use for retrieving 
        /// <seealso cref="MedicalModels.Provider"/> records from the repository
        /// </param>
        /// <param name="providerRequestRepository">
        /// The <seealso cref="IRepository{T}"/> to use for adding, deleting, and updating 
        /// <seealso cref="MedicalModels.Provider"/> records in the  repository
        /// </param>
        /// <param name="logger">
        /// The <seealso cref="ILogger{T}"/> to use to log messages
        /// </param>
        /// <param name="kernel">
        /// The <seealso cref="IKernel"/> to use for dependency injection
        /// </param>
        public Providers(
            IReadOnlyRepository<MedicalModels.Provider> providerReadOnlyRepository,
            IRepository<MedicalModels.Provider> providerRequestRepository,
            ILogger<Providers> logger,
            IKernel kernel)
        {
            this.providerReadOnlyRepository = providerReadOnlyRepository;
            this.providerRequestRepository = providerRequestRepository;
            this.logger = logger;
            this.kernel = kernel;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Adds an <seealso cref="MedicalModels.Provider"/> to the repository
        /// </summary>
        /// <param name="provider">
        /// The <seealso cref="MedicalModels.Provider"/> to add.
        /// </param>
        /// <param name="identity">
        /// The identity of the user authorized to add the authorization request to the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="provider"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to add the 
        /// <seealso cref="MedicalModels.Provider"/> to the repository
        /// </exception>
        public void CreateProvider(MedicalModels.Provider provider, IIdentity identity)
        {
            logger.EnterMethod("CreateProvider");

            Invariant.IsNotNull(provider, "provider");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                var accountId =
                    kernel.Get<IReadOnlyRepository<MedicalModels.Facility>>().FindBy(
                        facility => facility.Id.Equals(provider.FacilityId)).AccountId;
                kernel.Get<Security>().AuthorizeAction(identity, accountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in CreateProvider");
                throw;
            }

            try
            {
                providerRequestRepository.Add(provider);
            }
            catch (Exception exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("CreateProvider");
        }

        /// <summary>
        /// Deletes an <seealso cref="MedicalModels.Provider"/> from the repository
        /// </summary>
        /// <param name="provider">
        /// The <seealso cref="MedicalModels.Provider"/> to delete from the repository
        /// </param>
        /// <param name="identity">
        /// The <c>IIdentity</c> of the user authorized to delete the 
        /// <seealso cref="MedicalModels.Provider"/> from the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="provider"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to delete the provider from the repository
        /// </exception>
        public void DeleteProvider(MedicalModels.Provider provider, IIdentity identity)
        {
            logger.EnterMethod("DeleteProvider");

            Invariant.IsNotNull(provider, "provider");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                var accountId =
                    kernel.Get<IReadOnlyRepository<MedicalModels.Facility>>().FindBy(
                        facility => facility.Id.Equals(provider.FacilityId)).AccountId;
                kernel.Get<Security>().AuthorizeAction(identity, accountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in DeleteProvider");
                throw;
            }

            try
            {
                providerRequestRepository.Delete(provider);
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("DeleteProvider");
        }

        /// <summary>
        /// Updates an <seealso cref="MedicalModels.Provider"/> in the repository
        /// </summary>
        /// <param name="provider">
        /// The <seealso cref="MedicalModels.Provider"/> to update in the repository
        /// </param>
        /// <param name="identity">
        /// The <c>IIdentity</c> of the user authorized to update the 
        /// <seealso cref="MedicalModels.Provider"/> in the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="provider"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to update the provider in
        /// the repository
        /// </exception>
        public void UpdateProvider(MedicalModels.Provider provider, IIdentity identity)
        {
            logger.EnterMethod("UpdateProvider");

            Invariant.IsNotNull(provider, "provider");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                var accountId =
                    kernel.Get<IReadOnlyRepository<MedicalModels.Facility>>().FindBy(
                        facility => facility.Id.Equals(provider.FacilityId)).AccountId;
                kernel.Get<Security>().AuthorizeAction(identity, accountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in UpdateProvider");
                throw;
            }

            try
            {
                providerRequestRepository.Update(provider);
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("UpdateProvider");
        }

        #region Provider retrieval

        /// <summary>
        /// Retrieves an <seealso cref="MedicalModels.Provider"/> from the 
        /// repository using the id of the request
        /// </summary>
        /// <param name="providerId">
        /// The id of the <seealso cref="MedicalModels.Provider"/> to retrieve
        /// </param>
        /// <param name="identity">
        /// The identity whose credentials are used to authorize the action
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the <paramref name="providerId"/> parameter is less than 1
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to retrieve the provider in
        /// the repository
        /// </exception>
        /// <returns>
        /// The retrieved <seealso cref="MedicalModels.Provider"/> if it is 
        /// found in the repository or null if it is not found
        /// </returns>
        public MedicalModels.Provider GetProviderById(int providerId, IIdentity identity)
        {
            logger.EnterMethod("GetProviderById");

            Invariant.IsNotNull(identity, "identity");

            if (providerId < 1)
            {
                throw new ArgumentOutOfRangeException("providerId", providerId, "The providerId parameter must be greater than 0.");
            }

            var requestedProvider = providerReadOnlyRepository.FindBy(provider => provider.Id.Equals(providerId));

            try
            {
                var accountId =
                    kernel.Get<IReadOnlyRepository<MedicalModels.Facility>>().FindBy(
                        facility => facility.Id.Equals(requestedProvider.FacilityId)).AccountId;
                kernel.Get<Security>().AuthorizeAction(identity, accountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in UpdateProvider");
                throw;
            }

            logger.LeaveMethod("GetProviderById");
            return requestedProvider;
        }

        /// <summary>
        /// Retrieves a collection of <seealso cref="MedicalModels.Provider"/>
        /// that have an facility id that matches the <paramref name="facilityId"/> passed in
        /// </summary>
        /// <param name="facilityId">
        /// The id of the facility to retrieve the <seealso cref="MedicalModels.Provider"/> for
        /// </param>
        /// <param name="identity">
        /// The <seealso cref="IIdentity"/> that contains the identity of the user that is authorized
        /// to retrieve the records from the repository
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the <paramref name="facilityId"/> parameter is less than 1
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to retrieve the providers for the facility specified
        /// from the repository
        /// </exception>
        /// <returns>
        /// An <seealso cref="IQueryable{T}"/> collection of <seealso cref="MedicalModels.Provider"/>
        /// that belong to facility identified by <paramref name="facilityId"/>
        /// </returns>
        public IQueryable<MedicalModels.Provider> GetProvidersByFacility(int facilityId, IIdentity identity)
        {
            logger.EnterMethod("GetProvidersByFacility");

            Invariant.IsNotNull(identity, "identity");

            if (facilityId < 1)
            {
                throw new ArgumentOutOfRangeException("facilityId", facilityId, "The facilityId parameter must be greater than 0.");
            }

            try
            {
                var accountId =
                    kernel.Get<IReadOnlyRepository<MedicalModels.Facility>>().FindBy(
                        facility => facility.Id.Equals(facilityId)).AccountId;    
                kernel.Get<Security>().AuthorizeAction(identity, accountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in GetProvidersByFacility");
                throw;
            }

            var providers = providerReadOnlyRepository.FilterBy(provider => provider.FacilityId.Equals(facilityId));

            logger.LeaveMethod("GetProvidersByFacility");

            return providers;
        }

        /// <summary>
        /// Retrieves a collection of <seealso cref="MedicalModels.Provider"/>
        /// that have an account id that matches the <paramref name="accountId"/> passed in
        /// </summary>
        /// <param name="accountId">
        /// The id of the account to retrieve the <seealso cref="MedicalModels.Provider"/> for
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
        /// Thrown if the user is not authorized to retrieve the providers for the account specified
        /// from the repository
        /// </exception>
        /// <returns>
        /// An <seealso cref="IQueryable{T}"/> collection of <seealso cref="MedicalModels.Provider"/>
        /// that belong to account identified by <paramref name="accountId"/>
        /// </returns>
        public IQueryable<MedicalModels.Provider> GetProvidersByAccount(int accountId, IIdentity identity)
        {
            logger.EnterMethod("GetProvidersByAccount");

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
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in GetProvidersByAccount");
                throw;
            }

            var facilityIds =
                kernel.Get<IReadOnlyRepository<MedicalModels.Facility>>().FilterBy(
                    facility => facility.AccountId.Equals(accountId)).Select(facility => facility.Id).ToList();

            var providers =
                providerReadOnlyRepository.FilterBy(provider => facilityIds.Contains(provider.FacilityId));

            logger.LeaveMethod("GetProvidersByAccount");

            return providers;
        }

        #endregion

        #endregion
    }
}
