// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Medications.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Business logic for working with Medication objects
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BusinessLogic.Prescription
{
    using System;
    using System.Linq;
    using System.Security;
    using System.Security.Principal;

    using BusinessLogic.Helpers;

    using Data.Repository;

    using Infrastructure.Logging;

    using Ninject;

    using PrescriptionModels = Infrastructure.Model.Prescription;

    /// <summary>
    /// Business logic for working with <seealso cref="PrescriptionModels.Medication"/> objects
    /// </summary>
    public class Medications
    {
        #region private members

        /// <summary>
        /// The <seealso cref="IReadOnlyRepository{T}"/> for the 
        /// <seealso cref="PrescriptionModels.Medication"/> models
        /// </summary>
        private readonly IReadOnlyRepository<PrescriptionModels.Medication> medicationReadOnlyRepository;

        /// <summary>
        /// The <seealso cref="IRepository{T}"/> for the 
        /// <seealso cref="PrescriptionModels.Medication"/> models
        /// </summary>
        private readonly IRepository<PrescriptionModels.Medication> medicationRepository;

        /// <summary>
        /// The <seealso cref="ILogger{T}"/> to use for logging messages
        /// </summary>
        private readonly ILogger<Medications> logger;

        /// <summary>
        /// The object that implements the <seealso cref="IKernel"/> interface used for
        /// dependency injection
        /// </summary>
        private readonly IKernel kernel;

        #endregion

        #region public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Medications"/> class.
        /// Interfaces are used for initialization to facilitate dependency injection.
        /// </summary>
        /// <param name="medicationReadOnlyRepository">
        /// The <seealso cref="IReadOnlyRepository{T}"/> to use for retrieving 
        /// <seealso cref="PrescriptionModels.Medication"/> records from the repository
        /// </param>
        /// <param name="medicationRepository">
        /// The <seealso cref="IRepository{T}"/> to use for adding, deleting, and updating 
        /// <seealso cref="PrescriptionModels.Medication"/> records in the  repository
        /// </param>
        /// <param name="logger">
        /// The <seealso cref="ILogger{T}"/> to use to log messages
        /// </param>
        /// <param name="kernel">
        /// The <seealso cref="IKernel"/> to use for dependency injection
        /// </param>
        public Medications(
            IReadOnlyRepository<PrescriptionModels.Medication> medicationReadOnlyRepository,
            IRepository<PrescriptionModels.Medication> medicationRepository,
            ILogger<Medications> logger,
            IKernel kernel)
        {
            this.medicationReadOnlyRepository = medicationReadOnlyRepository;
            this.medicationRepository = medicationRepository;
            this.logger = logger;
            this.kernel = kernel;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Adds a <seealso cref="PrescriptionModels.Medication"/> to the repository
        /// </summary>
        /// <param name="medication">
        /// The <seealso cref="PrescriptionModels.Medication"/> to add.
        /// </param>
        /// <param name="identity">
        /// The identity of the user authorized to add the <seealso cref="PrescriptionModels.Medication"/>
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="medication"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to add the <seealso cref="PrescriptionModels.Medication"/>
        /// to the repository
        /// </exception>
        public void CreateMedication(PrescriptionModels.Medication medication, IIdentity identity)
        {
            logger.EnterMethod("CreateMedication");

            Invariant.IsNotNull(medication, "medication");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, medication.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in CreateMedication");
                throw;
            }

            try
            {
                medicationRepository.Add(medication);
            }
            catch (Exception exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("CreateMedication");
        }

        /// <summary>
        /// Deletes a <seealso cref="PrescriptionModels.Medication"/> from the repository
        /// </summary>
        /// <param name="medication">
        /// The <seealso cref="PrescriptionModels.Medication"/> to 
        /// delete from the repository
        /// </param>
        /// <param name="identity">
        /// The <c>IIdentity</c> of the user authorized to delete the 
        /// <seealso cref="PrescriptionModels.Medication"/> from the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="medication"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not  authorized to delete the <seealso cref="PrescriptionModels.Medication"/>
        /// from the repository
        /// </exception>
        public void DeleteMedication(PrescriptionModels.Medication medication, IIdentity identity)
        {
            logger.EnterMethod("DeleteMedication");

            Invariant.IsNotNull(medication, "medication");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, medication.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in DeleteMedication");
                throw;
            }

            try
            {
                medicationRepository.Delete(medication);
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("DeleteMedication");
        }

        /// <summary>
        /// Updates a <seealso cref="PrescriptionModels.Medication"/> in the repository
        /// </summary>
        /// <param name="medication">
        /// The <seealso cref="PrescriptionModels.Medication"/> to update in the repository
        /// </param>
        /// <param name="identity">
        /// The <c>IIdentity</c> of the user authorized to update the 
        /// <seealso cref="PrescriptionModels.Medication"/> in the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="medication"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to update the <seealso cref="PrescriptionModels.Medication"/>
        /// in the repository
        /// </exception>
        public void UpdateMedication(PrescriptionModels.Medication medication, IIdentity identity)
        {
            logger.EnterMethod("UpdateMedication");

            Invariant.IsNotNull(medication, "medication");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, medication.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in UpdateMedication");
                throw;
            }

            try
            {
                medicationRepository.Update(medication);
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("UpdateMedication");
        }

        #region medication retrieval

        /// <summary>
        /// Retrieves an <seealso cref="PrescriptionModels.Medication"/> from the 
        /// repository using the id of the medication
        /// </summary>
        /// <param name="medicationId">
        /// The id of the medication to retrieve
        /// </param>
        /// <param name="identity">
        /// The identity whose credentials are used to authorize the action
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the <paramref name="medicationId"/> parameter is less than 1
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to retrieve the medication in
        /// the repository
        /// </exception>
        /// <returns>
        /// The retrieved <seealso cref="PrescriptionModels.Medication"/> if it is 
        /// found in the repository or null if it is not found
        /// </returns>
        public PrescriptionModels.Medication GetMedicationById(int medicationId, IIdentity identity)
        {
            logger.EnterMethod("GetMedicationById");

            Invariant.IsNotNull(identity, "identity");

            if (medicationId < 1)
            {
                throw new ArgumentOutOfRangeException("medicationId", medicationId, "The medicationId parameter must be greater than 0.");
            }

            var requestedMedication = medicationReadOnlyRepository.FindBy(medication => medication.Id.Equals(medicationId));

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, requestedMedication.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in UpdateMedication");
                throw;
            }

            logger.LeaveMethod("GetMedicationById");
            return requestedMedication;
        }

        /// <summary>
        /// Retrieves a collection of <seealso cref="PrescriptionModels.Medication"/> 
        /// that have an account id that matches the <paramref name="accountId"/> passed in
        /// </summary>
        /// <param name="accountId">
        /// The id of the account to retrieve the <seealso cref="PrescriptionModels.Medication"/> for
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
        /// Thrown if the user is not authorized to retrieve the <seealso cref="PrescriptionModels.Medication"/>
        /// for the account specified from the repository
        /// </exception>
        /// <returns>
        /// An <seealso cref="IQueryable{T}"/> collection of <seealso cref="PrescriptionModels.Medication"/>
        /// that belong to account identified by <paramref name="accountId"/>
        /// </returns>
        public IQueryable<PrescriptionModels.Medication> GetMedicationsByAccount(int accountId, IIdentity identity)
        {
            logger.EnterMethod("GetMedicationsByAccount");

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
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in GetMedicationsByAccount");
                throw;
            }

            var insurers = medicationReadOnlyRepository.FilterBy(medication => medication.AccountId.Equals(accountId));

            logger.LeaveMethod("GetMedicationsByAccount");

            return insurers;
        }

        #endregion

        #endregion
    }
}
