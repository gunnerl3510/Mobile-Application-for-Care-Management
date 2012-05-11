// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrescriptionPickups.cs" company="">
//   
// </copyright>
// <summary>
//   Business logic for working with PrescriptionPickup objects
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BusinessLogic.Prescription
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security;
    using System.Security.Principal;
    using System.Text;
    using System.Web.Security;

    using BusinessLogic.Helpers;

    using Data.Repository;

    using Infrastructure.Logging;

    using Ninject;

    using AccountModels = Infrastructure.Model.Account;
    using PrescriptionModels = Infrastructure.Model.Prescription;

    /// <summary>
    /// Business logic for working with <seealso cref="PrescriptionModels.PrescriptionPickup"/> objects
    /// </summary>
    public class PrescriptionPickups
    {
        #region private members

        /// <summary>
        /// The <seealso cref="IReadOnlyRepository{T}"/> for the 
        /// <seealso cref="PrescriptionModels.PrescriptionPickup"/> models
        /// </summary>
        private readonly IReadOnlyRepository<PrescriptionModels.PrescriptionPickup> prescriptionPickupkReadOnlyRepository;

        /// <summary>
        /// The <seealso cref="IRepository{T}"/> for the 
        /// <seealso cref="PrescriptionModels.PrescriptionPickup"/> models
        /// </summary>
        private readonly IRepository<PrescriptionModels.PrescriptionPickup> prescriptionPickupRepository;

        /// <summary>
        /// The <seealso cref="ILogger{T}"/> to use for logging messages
        /// </summary>
        private readonly ILogger<PrescriptionPickups> logger;

        /// <summary>
        /// The object that implements the <seealso cref="IKernel"/> interface used for
        /// dependency injection
        /// </summary>
        private readonly IKernel kernel;

        #endregion

        #region public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PrescriptionPickups"/> class.
        /// Interfaces are used for initialization to facilitate dependency injection.
        /// </summary>
        /// <param name="prescriptionPickupkReadOnlyRepository">
        /// The <seealso cref="IReadOnlyRepository{T}"/> to use for retrieving 
        /// <seealso cref="PrescriptionModels.PrescriptionPickup"/> records from the repository
        /// </param>
        /// <param name="prescriptionPickupRepository">
        /// The <seealso cref="IRepository{T}"/> to use for adding, deleting, and updating 
        /// <seealso cref="PrescriptionModels.PrescriptionPickup"/> records in the  repository
        /// </param>
        /// <param name="logger">
        /// The <seealso cref="ILogger{T}"/> to use to log messages
        /// </param>
        /// <param name="kernel">
        /// The <seealso cref="IKernel"/> to use for dependency injection
        /// </param>
        public PrescriptionPickups(
            IReadOnlyRepository<PrescriptionModels.PrescriptionPickup> prescriptionPickupkReadOnlyRepository,
            IRepository<PrescriptionModels.PrescriptionPickup> prescriptionPickupRepository,
            ILogger<PrescriptionPickups> logger,
            IKernel kernel)
        {
            this.prescriptionPickupkReadOnlyRepository = prescriptionPickupkReadOnlyRepository;
            this.prescriptionPickupRepository = prescriptionPickupRepository;
            this.logger = logger;
            this.kernel = kernel;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Adds an <seealso cref="PrescriptionModels.PrescriptionPickup"/> to the repository
        /// </summary>
        /// <param name="prescriptionPickup">
        /// The <seealso cref="PrescriptionModels.PrescriptionPickup"/> to add.
        /// </param>
        /// <param name="identity">
        /// The identity of the user authorized to add the <seealso cref="PrescriptionModels.PrescriptionPickup"/>
        /// to the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="prescriptionPickup"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to add the 
        /// <seealso cref="PrescriptionModels.PrescriptionPickup"/> to the repository
        /// </exception>
        public void CreatePrescriptionPickup(PrescriptionModels.PrescriptionPickup prescriptionPickup, IIdentity identity)
        {
            logger.EnterMethod("CreatePrescriptionPickup");

            Invariant.IsNotNull(prescriptionPickup, "prescriptionPickup");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, prescriptionPickup.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in CreatePrescriptionPickup");
                throw;
            }

            try
            {
                prescriptionPickupRepository.Add(prescriptionPickup);
            }
            catch (Exception exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("CreatePrescriptionPickup");
        }

        /// <summary>
        /// Deletes a <seealso cref="PrescriptionModels.PrescriptionPickup"/> from the repository
        /// </summary>
        /// <param name="prescriptionPickup">
        /// The <seealso cref="PrescriptionModels.PrescriptionPickup"/> to delete from the repository
        /// </param>
        /// <param name="identity">
        /// The <c>IIdentity</c> of the user authorized to delete the 
        /// <seealso cref="PrescriptionModels.PrescriptionPickup"/> from the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="prescriptionPickup"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to delete the authorization follow up from the repository
        /// </exception>
        public void DeletePrescriptionPickup(PrescriptionModels.PrescriptionPickup prescriptionPickup, IIdentity identity)
        {
            logger.EnterMethod("DeletePrescriptionPickup");

            Invariant.IsNotNull(prescriptionPickup, "prescriptionPickup");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, prescriptionPickup.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in DeletePrescriptionPickup");
                throw;
            }

            try
            {
                prescriptionPickupRepository.Delete(prescriptionPickup);
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("DeletePrescriptionPickup");
        }

        /// <summary>
        /// Updates an <seealso cref="PrescriptionModels.PrescriptionPickup"/> in the repository
        /// </summary>
        /// <param name="prescriptionPickup">
        /// The <seealso cref="PrescriptionModels.PrescriptionPickup"/> to update in the repository
        /// </param>
        /// <param name="identity">
        /// The <c>IIdentity</c> of the user authorized to update the 
        /// <seealso cref="PrescriptionModels.PrescriptionPickup"/> in the repository
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="prescriptionPickup"/> parameter is null
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to update the <seealso cref="PrescriptionModels.PrescriptionPickup"/>
        /// in the repository
        /// </exception>
        public void UpdatePrescriptionPickup(PrescriptionModels.PrescriptionPickup prescriptionPickup, IIdentity identity)
        {
            logger.EnterMethod("UpdatePrescriptionPickup");

            Invariant.IsNotNull(prescriptionPickup, "prescriptionPickup");
            Invariant.IsNotNull(identity, "identity");

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, prescriptionPickup.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in UpdatePrescriptionPickup");
                throw;
            }

            try
            {
                prescriptionPickupRepository.Update(prescriptionPickup);
            }
            catch (ArgumentException exception)
            {
                logger.LogException(exception);
                throw;
            }

            logger.LeaveMethod("UpdatePrescriptionPickup");
        }

        #region PrescriptionPickup retrieval

        /// <summary>
        /// Retrives an <seealso cref="IQueryable{T}"/> of <seealso cref="PrescriptionModels.PrescriptionPickup"/> from
        /// the repository.
        /// </summary>
        /// <param name="identity">
        /// The identity of the user requesting the prescription pickups.
        /// </param>
        /// <returns>
        /// An <seealso cref="IQueryable{T}"/> of <seealso cref="PrescriptionModels.PrescriptionPickup"/>.
        /// </returns>
        public IQueryable<PrescriptionModels.PrescriptionPickup> GetPrescriptionPickups(IIdentity identity)
        {
            logger.EnterMethod("GetPrescriptionPickups");

            Invariant.IsNotNull(identity, "identity");

            IQueryable<PrescriptionModels.PrescriptionPickup> pickups;

            if (Roles.IsUserInRole(identity.Name, "Admin"))
            {
                pickups = prescriptionPickupkReadOnlyRepository.All();
            }
            else
            {
                var user = Membership.GetUser(identity.Name, false);
                var accountReadRepository = kernel.Get<IReadOnlyRepository<AccountModels.Account>>();
                var userAccount = accountReadRepository.FindBy(account => account.UserId.Value.Equals((Guid)user.ProviderUserKey));

                pickups = prescriptionPickupkReadOnlyRepository.FilterBy(pickup => pickup.AccountId.Equals(userAccount.Id));
            }

            logger.LeaveMethod("GetPrescriptionPickups");

            return pickups;
        }

        /// <summary>
        /// Retrieves an <seealso cref="PrescriptionModels.PrescriptionPickup"/> from the 
        /// repository using the id of the pickup appointment
        /// </summary>
        /// <param name="prescriptionPickupId">
        /// The id of the <seealso cref="PrescriptionModels.PrescriptionPickup"/> to retrieve
        /// </param>
        /// <param name="identity">
        /// The identity whose credentials are used to authorize the action
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the <paramref name="prescriptionPickupId"/> parameter is less than 1
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to retrieve the 
        /// <seealso cref="PrescriptionModels.PrescriptionPickup"/> in the repository
        /// </exception>
        /// <returns>
        /// The retrieved <seealso cref="PrescriptionModels.PrescriptionPickup"/> if it is 
        /// found in the repository or null if it is not found
        /// </returns>
        public PrescriptionModels.PrescriptionPickup GetPrescriptionPickupById(int prescriptionPickupId, IIdentity identity)
        {
            logger.EnterMethod("GetPrescriptionPickupById");

            Invariant.IsNotNull(identity, "identity");

            if (prescriptionPickupId < 1)
            {
                throw new ArgumentOutOfRangeException("prescriptionPickupId", prescriptionPickupId, "The prescriptionPickupId parameter must be greater than 0.");
            }

            var requestedPrescriptionPickup = prescriptionPickupkReadOnlyRepository.FindBy(prescriptionPickup => prescriptionPickup.Id.Equals(prescriptionPickupId));

            try
            {
                kernel.Get<Security>().AuthorizeAction(identity, requestedPrescriptionPickup.AccountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in UpdatePrescriptionPickup");
                throw;
            }

            logger.LeaveMethod("GetPrescriptionPickupById");
            return requestedPrescriptionPickup;
        }

        /// <summary>
        /// Retrieves a collection of <seealso cref="PrescriptionModels.PrescriptionPickup"/>
        /// that have an medication id that matches the <paramref name="medicationId"/>
        /// passed in
        /// </summary>
        /// <param name="medicationId">
        /// The id of the authorization request to retrieve the <seealso cref="PrescriptionModels.PrescriptionPickup"/> for
        /// </param>
        /// <param name="identity">
        /// The <seealso cref="IIdentity"/> that contains the identity of the user that is authorized
        /// to retrieve the records from the repository
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the <paramref name="medicationId"/> parameter is less than 1
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the <paramref name="identity"/> parameter is null
        /// </exception>
        /// <exception cref="SecurityException">
        /// Thrown if the user is not authorized to retrieve the <seealso cref="PrescriptionModels.PrescriptionPickup"/>
        /// for the medication specified from the repository
        /// </exception>
        /// <returns>
        /// An <seealso cref="IQueryable{T}"/> collection of <seealso cref="PrescriptionModels.PrescriptionPickup"/>
        /// that belong to the authorization request identified by <paramref name="medicationId"/>
        /// </returns>
        public IQueryable<PrescriptionModels.PrescriptionPickup> GetPrescriptionPickupsByMedication(int medicationId, IIdentity identity)
        {
            logger.EnterMethod("GetPrescriptionPickupsByMedication");

            Invariant.IsNotNull(identity, "identity");

            if (medicationId < 1)
            {
                throw new ArgumentOutOfRangeException("medicationId", medicationId, "The medicationId parameter must be greater than 0.");
            }

            try
            {
                var accountId =
                    kernel.Get<IReadOnlyRepository<PrescriptionModels.Medication>>().FindBy(
                        medication => medication.Id.Equals(medicationId)).AccountId;    
                kernel.Get<Security>().AuthorizeAction(identity, accountId);
            }
            catch (SecurityException exception)
            {
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in GetPrescriptionPickupsByMedication");
                throw;
            }

            var prescriptionPickups =
                prescriptionPickupkReadOnlyRepository.FilterBy(
                    prescriptionPickup => prescriptionPickup.MedicationId.Equals(medicationId));

            logger.LeaveMethod("GetPrescriptionPickupsByMedication");

            return prescriptionPickups;
        }

        /// <summary>
        /// Retrieves a collection of <seealso cref="PrescriptionModels.PrescriptionPickup"/>
        /// for the account identified by the <paramref name="accountId"/> passed in
        /// </summary>
        /// <param name="accountId">
        /// The id of the account to retrieve the <seealso cref="PrescriptionModels.PrescriptionPickup"/> for
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
        /// Thrown if the user is not authorized to retrieve the <seealso cref="PrescriptionModels.PrescriptionPickup"/>
        /// for the account specified from the repository
        /// </exception>
        /// <returns>
        /// An <seealso cref="IQueryable{T}"/> collection of <seealso cref="PrescriptionModels.PrescriptionPickup"/>
        /// that belong to the account identified by <paramref name="accountId"/>
        /// </returns>
        public IQueryable<PrescriptionModels.PrescriptionPickup> GetPrescriptionPickupsByAccount(int accountId, IIdentity identity)
        {
            logger.EnterMethod("GetPrescriptionPickupsByAccount");

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
                logger.LogExceptionWithMessage(exception, "SecurityException thrown in GetPrescriptionPickupsByAccount");
                throw;
            }

            var prescriptionPickups =
                prescriptionPickupkReadOnlyRepository.FilterBy(prescriptionPickup => prescriptionPickup.AccountId.Equals(accountId));

            logger.LeaveMethod("GetPrescriptionPickupsByAccount");

            return prescriptionPickups;
        }

        #endregion

        #endregion
    }
}
