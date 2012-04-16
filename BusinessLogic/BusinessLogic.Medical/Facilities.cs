// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Facilities.cs" company="LC LLC">
//   All rights reserved
// </copyright>
// <summary>
//   Business logic for working with Facility objects
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
    /// Business logic for working with <seealso cref="MedicalModels.Facility"/> objects
    /// </summary>
    public class Facilities
    {
        #region private members

        /// <summary>
        /// The <seealso cref="IReadOnlyRepository{T}"/> for the 
        /// <seealso cref="MedicalModels.Facility"/> models
        /// </summary>
        private readonly IReadOnlyRepository<MedicalModels.Facility> facilityReadOnlyRepository;

        /// <summary>
        /// The <seealso cref="IRepository{T}"/> for the 
        /// <seealso cref="MedicalModels.Facility"/> models
        /// </summary>
        private readonly IRepository<MedicalModels.Facility> facilityRepository;

        /// <summary>
        /// The <seealso cref="ILogger{T}"/> to use for logging messages
        /// </summary>
        private readonly ILogger<Facilities> logger;

        /// <summary>
        /// The object that implements the <seealso cref="IKernel"/> interface used for
        /// dependency injection
        /// </summary>
        private readonly IKernel kernel;

        #endregion

        #region public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Facilities"/> class.
        /// Interfaces are used for initialization to facilitate dependency injection.
        /// </summary>
        /// <param name="facilityReadOnlyRepository">
        /// The <seealso cref="IReadOnlyRepository{T}"/> to use for retrieving 
        /// <seealso cref="MedicalModels.Facility"/> records from the repository 
        /// </param>
        /// <param name="facilityRepository">
        /// The <seealso cref="IRepository{T}"/> to use for adding, deleting, and updating 
        /// <seealso cref="MedicalModels.Facility"/> records in the  repository
        /// </param>
        /// <param name="logger">
        /// The <c>ILogger</c> to user to log messages
        /// </param>
        /// <param name="kernel">
        /// The <c>IKernel</c> to use for dependency injection
        /// </param>
        public Facilities(
            IReadOnlyRepository<MedicalModels.Facility> facilityReadOnlyRepository,
            IRepository<MedicalModels.Facility> facilityRepository,
            ILogger<Facilities> logger,
            IKernel kernel)
        {
            this.facilityReadOnlyRepository = facilityReadOnlyRepository;
            this.facilityRepository = facilityRepository;
            this.logger = logger;
            this.kernel = kernel;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Adds a <seealso cref="MedicalModels.Facility"/> to the repository
        /// </summary>
        /// <param name="facility">
        /// The <seealso cref="MedicalModels.Facility"/> to add.
        /// </param>
        /// <param name="identity">
        /// The identity of the user authorized to add the <seealso cref="MedicalModels.Facility"/>
        ///  to the account
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="facility"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to add the <seealso cref="MedicalModels.Facility"/>
        ///  to the account
        /// </exception>
        public void CreateFacility(MedicalModels.Facility facility, IIdentity identity)
        {
            logger.EnterMethod("CreateFacility");

            Invariant.IsNotNull(facility, "facility");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, facility.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in CreateFacility");
                throw;
            }

            try
            {
                facilityRepository.Add(facility);
            }
            catch (Exception exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("CreateFacility");
        }

        /// <summary>
        /// Deletes a <seealso cref="MedicalModels.Facility"/> from the repository
        /// </summary>
        /// <param name="facility">
        /// The <seealso cref="MedicalModels.Facility"/> to delete from the repository
        /// </param>
        /// <param name="identity">
        /// The <c>IIdentity</c> of the user authorized to delete the 
        /// <seealso cref="MedicalModels.Facility"/> from the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="facility"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not  authorized to delete the <seealso cref="MedicalModels.Facility"/>
        /// from the repository
        /// </exception>
        public void DeleteFacility(MedicalModels.Facility facility, IIdentity identity)
        {
            logger.EnterMethod("DeleteFacility");

            Invariant.IsNotNull(facility, "facility");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, facility.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in DeleteFacility");
                throw;
            }

            try
            {
                this.facilityRepository.Delete(facility);
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("DeleteFacility");
        }

        /// <summary>
        /// Updates a <seealso cref="MedicalModels.Facility"/> in the repository
        /// </summary>
        /// <param name="facility">
        /// The <seealso cref="MedicalModels.Facility"/> to update in the repository
        /// </param>
        /// <param name="identity">
        /// The <c>IIdentity</c> of the user authorized to update the 
        /// <seealso cref="MedicalModels.Facility"/> in the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="facility"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to update the <seealso cref="MedicalModels.Facility"/>
        /// in the repository
        /// </exception>
        public void UpdateFacility(MedicalModels.Facility facility, IIdentity identity)
        {
            logger.EnterMethod("UpdateFacility");

            Invariant.IsNotNull(facility, "facility");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, facility.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in UpdateFacility");
                throw;
            }

            try
            {
                facilityRepository.Update(facility);
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("UpdateFacility");
        }

        #region facility retrieval

        /// <summary>
        /// Retrieves a <seealso cref="MedicalModels.Facility"/> from the 
        /// repository using the id of the facility
        /// </summary>
        /// <param name="facilityId">
        /// The id of the <seealso cref="MedicalModels.Facility"/> to retrieve
        /// </param>
        /// <param name="identity">
        /// The identity whose credentials are used to authorize the action
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the <paramref name="facilityId"/> parameter is less than 1
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to retrieve the 
        /// <seealso cref="MedicalModels.Facility"/> from the repository
        /// </exception>
        /// <returns>
        /// The retrieved <seealso cref="MedicalModels.Facility"/> if it is 
        /// found in the repository or null if it is not found
        /// </returns>
        public MedicalModels.Facility GetFacilityById(int facilityId, IIdentity identity)
        {
            logger.EnterMethod("GetFacilityById");

            Invariant.IsNotNull(identity, "identity");

            if (facilityId < 1)
            {
                throw new ArgumentOutOfRangeException("facilityId", facilityId, "The facilityId parameter must be greater than 0.");
            }

            var requestedFacility = facilityReadOnlyRepository.FindBy(facility => facility.Id.Equals(facilityId));

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, requestedFacility.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in UpdateFacility");
                throw;
            }

            logger.LeaveMethod("GetFacilityById");
            return requestedFacility;
        }

        /// <summary>
        /// Retrieves a collection of <seealso cref="MedicalModels.Facility"/> 
        /// that have an account id that matches the <paramref name="accountId"/> passed in
        /// </summary>
        /// <param name="accountId">
        /// The id of the account to retrieve the <seealso cref="MedicalModels.Facility"/> for
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
        /// Thrown if the user is not authorized to retrieve the <seealso cref="MedicalModels.Facility"/>
        /// for the account specified from the repository
        /// </exception>
        /// <returns>
        /// An <seealso cref="IQueryable{T}"/> collection of <seealso cref="MedicalModels.Facility"/>
        /// that belong to account identified by <paramref name="accountId"/>
        /// </returns>
        public IQueryable<MedicalModels.Facility> GetFacilitiesByAccount(int accountId, IIdentity identity)
        {
            logger.EnterMethod("GetFacilitiesByAccount");

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
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in GetFacilitiesByAccount");
                throw;
            }

            var facilities = facilityReadOnlyRepository.FilterBy(facility => facility.AccountId.Equals(accountId));

            logger.LeaveMethod("GetFacilitiesByAccount");

            return facilities;
        }

        #endregion

        #endregion
    }
}
