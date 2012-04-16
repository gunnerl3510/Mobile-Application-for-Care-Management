// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MedicalAppointments.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Business logic for working with MedicalAppointment objects
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BusinessLogic.Medical
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security;
    using System.Security.Principal;
    using System.Text;

    using BusinessLogic.Helpers;

    using Data.Repository;

    using Infrastructure.Logging;

    using Ninject;

    using MedicalModels = Infrastructure.Model.Medical;

    /// <summary>
    /// Business logic for working with <seealso cref="MedicalModels.MedicalAppointment"/> objects
    /// </summary>
    public class MedicalAppointments
    {
        #region private members

        /// <summary>
        /// The <seealso cref="IReadOnlyRepository{T}"/> for the 
        /// <seealso cref="MedicalModels.MedicalAppointment"/> models
        /// </summary>
        private readonly IReadOnlyRepository<MedicalModels.MedicalAppointment> medicalAppointmentReadOnlyRepository;

        /// <summary>
        /// The <seealso cref="IRepository{T}"/> for the 
        /// <seealso cref="MedicalModels.MedicalAppointment"/> models
        /// </summary>
        private readonly IRepository<MedicalModels.MedicalAppointment> medicalAppointmentRepository;

        /// <summary>
        /// The <seealso cref="ILogger{T}"/> to use for logging messages
        /// </summary>
        private readonly ILogger<MedicalAppointments> logger;

        /// <summary>
        /// The object that implements the <seealso cref="IKernel"/> interface used for
        /// dependency injection
        /// </summary>
        private readonly IKernel kernel;

        #endregion

        #region public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicalAppointments"/> class.
        /// Interfaces are used for initialization to facilitate dependency injection.
        /// </summary>
        /// <param name="medicalAppointmentReadOnlyRepository">
        /// The <seealso cref="IReadOnlyRepository{T}"/> to use for retrieving 
        /// <seealso cref="MedicalModels.MedicalAppointment"/> records from the repository
        /// </param>
        /// <param name="medicalAppointmentRepository">
        /// The <seealso cref="IRepository{T}"/> to use for adding, deleting, and updating 
        /// <seealso cref="MedicalModels.MedicalAppointment"/> records in the  repository
        /// </param>
        /// <param name="logger">
        /// The <seealso cref="ILogger{T}"/> to use to log messages
        /// </param>
        /// <param name="kernel">
        /// The <seealso cref="IKernel"/> to use for dependency injection
        /// </param>
        public MedicalAppointments(
            IReadOnlyRepository<MedicalModels.MedicalAppointment> medicalAppointmentReadOnlyRepository,
            IRepository<MedicalModels.MedicalAppointment> medicalAppointmentRepository,
            ILogger<MedicalAppointments> logger,
            IKernel kernel)
        {
            this.medicalAppointmentReadOnlyRepository = medicalAppointmentReadOnlyRepository;
            this.medicalAppointmentRepository = medicalAppointmentRepository;
            this.logger = logger;
            this.kernel = kernel;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Adds an <seealso cref="MedicalModels.MedicalAppointment"/> to the repository
        /// </summary>
        /// <param name="medicalAppointment">
        /// The <seealso cref="MedicalModels.MedicalAppointment"/> to add.
        /// </param>
        /// <param name="identity">
        /// The identity of the user authorized to add the authorization follow up to the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="medicalAppointment"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to add the 
        /// <seealso cref="MedicalModels.MedicalAppointment"/> to the repository
        /// </exception>
        public void CreateMedicalAppointment(MedicalModels.MedicalAppointment medicalAppointment, IIdentity identity)
        {
            logger.EnterMethod("CreateMedicalAppointment");

            Invariant.IsNotNull(medicalAppointment, "medicalAppointment");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, medicalAppointment.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in CreateMedicalAppointment");
                throw;
            }

            try
            {
                medicalAppointmentRepository.Add(medicalAppointment);
            }
            catch (Exception exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("CreateMedicalAppointment");
        }

        /// <summary>
        /// Deletes an <seealso cref="MedicalModels.MedicalAppointment"/> from the repository
        /// </summary>
        /// <param name="medicalAppointment">
        /// The <seealso cref="MedicalModels.MedicalAppointment"/> to delete from the repository
        /// </param>
        /// <param name="identity">
        /// The <c>IIdentity</c> of the user authorized to delete the 
        /// <seealso cref="MedicalModels.MedicalAppointment"/> from the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="medicalAppointment"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to delete the authorization medical appointment
        /// from the repository
        /// </exception>
        public void DeleteMedicalAppointment(MedicalModels.MedicalAppointment medicalAppointment, IIdentity identity)
        {
            logger.EnterMethod("DeleteMedicalAppointment");

            Invariant.IsNotNull(medicalAppointment, "medicalAppointment");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, medicalAppointment.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in DeleteMedicalAppointment");
                throw;
            }

            try
            {
                medicalAppointmentRepository.Delete(medicalAppointment);
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("DeleteMedicalAppointment");
        }

        /// <summary>
        /// Updates an <seealso cref="MedicalModels.MedicalAppointment"/> in the repository
        /// </summary>
        /// <param name="medicalAppointment">
        /// The <seealso cref="MedicalModels.MedicalAppointment"/> to update in the repository
        /// </param>
        /// <param name="identity">
        /// The <c>IIdentity</c> of the user authorized to update the 
        /// <seealso cref="MedicalModels.MedicalAppointment"/> in the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="medicalAppointment"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to update the medical appointment in
        /// the repository
        /// </exception>
        public void UpdateMedicalAppointment(MedicalModels.MedicalAppointment medicalAppointment, IIdentity identity)
        {
            logger.EnterMethod("UpdateMedicalAppointment");

            Invariant.IsNotNull(medicalAppointment, "medicalAppointment");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, medicalAppointment.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in UpdateMedicalAppointment");
                throw;
            }

            try
            {
                medicalAppointmentRepository.Update(medicalAppointment);
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("UpdateMedicalAppointment");
        }

        #region MedicalAppointment retrieval

        /// <summary>
        /// Retrieves a <seealso cref="MedicalModels.MedicalAppointment"/> from the 
        /// repository using the id of the appointment
        /// </summary>
        /// <param name="appointmentId">
        /// The id of the <seealso cref="MedicalModels.MedicalAppointment"/> to retrieve
        /// </param>
        /// <param name="identity">
        /// The identity whose credentials are used to authorize the action
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the <paramref name="appointmentId"/> parameter is less than 1
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to retrieve the 
        /// <seealso cref="MedicalModels.MedicalAppointment"/> in the repository
        /// </exception>
        /// <returns>
        /// The retrieved <seealso cref="MedicalModels.MedicalAppointment"/> if it is 
        /// found in the repository or null if it is not found
        /// </returns>
        public MedicalModels.MedicalAppointment GetMedicalAppointmentById(int appointmentId, IIdentity identity)
        {
            logger.EnterMethod("GetMedicalAppointmentById");

            Invariant.IsNotNull(identity, "identity");

            if (appointmentId < 1)
            {
                throw new ArgumentOutOfRangeException("appointmentId", appointmentId, "The appointmentId parameter must be greater than 0.");
            }

            var requestedAppointment = medicalAppointmentReadOnlyRepository.FindBy(appointment => appointment.Id.Equals(appointmentId));

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, requestedAppointment.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in UpdateMedicalAppointment");
                throw;
            }

            logger.LeaveMethod("GetMedicalAppointmentById");
            return requestedAppointment;
        }

        /// <summary>
        /// Retrieves a collection of <seealso cref="MedicalModels.MedicalAppointment"/>
        /// that have an provider id that matches the <paramref name="providerId"/> passed in
        /// </summary>
        /// <param name="providerId">
        /// The id of the provider to retrieve the <seealso cref="MedicalModels.MedicalAppointment"/> for
        /// </param>
        /// <param name="identity">
        /// The <seealso cref="IIdentity"/> that contains the identity of the user that is authorized
        /// to retrieve the records from the repository
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the <paramref name="providerId"/> parameter is less than 1
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to retrieve the medical appintments for the provider specified
        /// from the repository
        /// </exception>
        /// <returns>
        /// An <seealso cref="IQueryable{T}"/> collection of <seealso cref="MedicalModels.MedicalAppointment"/>
        /// that belong to the provider identified by <paramref name="providerId"/>
        /// </returns>
        public IQueryable<MedicalModels.MedicalAppointment> GetMedicalAppointmentsByProvider(int providerId, IIdentity identity)
        {
            logger.EnterMethod("GetMedicalAppointmentsByProvider");

            Invariant.IsNotNull(identity, "identity");

            if (providerId < 1)
            {
                throw new ArgumentOutOfRangeException("providerId", providerId, "The providerId parameter must be greater than 0.");
            }

            try
            {
                var facilityId =
                    kernel.Get<IReadOnlyRepository<MedicalModels.Provider>>().FindBy(
                        provider => provider.Id.Equals(providerId)).FacilityId;

                var accountId =
                    kernel.Get<IReadOnlyRepository<MedicalModels.Facility>>().FindBy(
                        facility => facility.Id.Equals(facilityId)).AccountId;

                kernel.Get<Security>().AuthorizeAction(identity, accountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in GetMedicalAppointmentsByProvider");
                throw;
            }

            var medicalAppointments =
                medicalAppointmentReadOnlyRepository.FilterBy(
                    appointment => appointment.ProviderId.Equals(providerId));

            logger.LeaveMethod("GetMedicalAppointmentsByProvider");

            return medicalAppointments;
        }

        /// <summary>
        /// Retrieves a collection of <seealso cref="MedicalModels.MedicalAppointment"/>
        /// that have an facility id that matches the <paramref name="facilityId"/> passed in
        /// </summary>
        /// <param name="facilityId">
        /// The id of the facility to retrieve the <seealso cref="MedicalModels.MedicalAppointment"/> for
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
        /// Thrown if the user is not authorized to retrieve the medical appintments for the facility specified
        /// from the repository
        /// </exception>
        /// <returns>
        /// An <seealso cref="IQueryable{T}"/> collection of <seealso cref="MedicalModels.MedicalAppointment"/>
        /// that belong to the facility identified by <paramref name="facilityId"/>
        /// </returns>
        public IQueryable<MedicalModels.MedicalAppointment> GetMedicalAppointmentsByFacility(int facilityId, IIdentity identity)
        {
            logger.EnterMethod("GetMedicalAppointmentsByFacility");

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
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in GetMedicalAppointmentsByFacility");
                throw;
            }

            var providerIds =
                kernel.Get<IReadOnlyRepository<MedicalModels.Facility>>().FilterBy(
                    facility => facility.Id.Equals(facilityId)).Select(facility => facility.Id).ToList();

            var medicalAppointments =
                medicalAppointmentReadOnlyRepository.FilterBy(
                    appointment => providerIds.Contains(appointment.ProviderId));

            logger.LeaveMethod("GetMedicalAppointmentsByFacility");

            return medicalAppointments;
        }

        /// <summary>
        /// Retrieves a collection of <seealso cref="MedicalModels.MedicalAppointment"/>
        /// for the account identified by the <paramref name="accountId"/> passed in
        /// </summary>
        /// <param name="accountId">
        /// The id of the account to retrieve the <seealso cref="MedicalModels.MedicalAppointment"/> for
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
        /// Thrown if the user is not authorized to retrieve the medical appointments for the account specified
        /// from the repository
        /// </exception>
        /// <returns>
        /// An <seealso cref="IQueryable{T}"/> collection of <seealso cref="MedicalModels.MedicalAppointment"/>
        /// that belong to the account identified by <paramref name="accountId"/>
        /// </returns>
        public IQueryable<MedicalModels.MedicalAppointment> GetMedicalAppointmentsByAccount(int accountId, IIdentity identity)
        {
            logger.EnterMethod("GetMedicalAppointmentsByAccount");

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
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in GetMedicalAppointmentsByAccount");
                throw;
            }

            var requests =
                medicalAppointmentReadOnlyRepository.FilterBy(appointment => appointment.AccountId.Equals(accountId));

            logger.LeaveMethod("GetMedicalAppointmentsByAccount");

            return requests;
        }

        #endregion

        #endregion
    }
}
