// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrescriptionService.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Encapsulates the service logic for working with objects in the Prescription domain
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Service.ServiceImplementation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Permissions;
    using System.ServiceModel;

    using BusinessLogic.Prescription;

    using Infrastructure.Security;

    using Ninject;

    /// <summary>
    /// Encapsulates the service logic for working with objects in the Medical domain
    /// </summary>
    public partial class PrescriptionService
    {
        /// <summary>
        /// The business logic object for manipulating facility objects
        /// </summary>
        private Medications medicationManager;

        /// <summary>
        /// The business logic object for manipulating provider objects
        /// </summary>
        private PrescriptionPickups pickupManager;

        /// <summary>
        /// The dependecy injection kernel
        /// </summary>
        private IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrescriptionService" />
        /// class
        /// </summary>
        /// <param name="kernel">
        /// The dependency injection object
        /// </param>
        public PrescriptionService(IKernel kernel)
        {
            this.kernel = kernel;
            medicationManager = this.kernel.Get<Medications>();
            pickupManager = this.kernel.Get<PrescriptionPickups>();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="PrescriptionService" />
        /// class from being created.
        /// </summary>
        private PrescriptionService()
        {
        }

        #region PrescriptionContract Members

        /// <summary>
        /// Adds a medication through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the medication to add
        /// </param>
        public override void AddMedication(Service.MessageContracts.MedicationRequestMessage request)
        {
            medicationManager.CreateMedication(request.Medication,  ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Deletes a medication through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the medication to delete
        /// </param>
        public override void DeleteMedication(Service.MessageContracts.MedicationRequestMessage request)
        {
            medicationManager.DeleteMedication(request.Medication,  ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Updates a medication through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the medication to update
        /// </param>
        public override void UpdateMedication(Service.MessageContracts.MedicationRequestMessage request)
        {
            medicationManager.UpdateMedication(request.Medication,  ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Gets a medication through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the medication id of the medication to retrieve
        /// </param>
        /// <returns>
        /// An message containing the requested medication
        /// </returns>
        public override Service.MessageContracts.MedicationMessage GetMedication(Service.MessageContracts.MedicationIdRequestMessage request)
        {
            return new Service.MessageContracts.MedicationMessage
            {
                Medication =
                    medicationManager
                        .GetMedicationById(
                            request.MedicationId,
                            ServiceSecurityContext.Current.PrimaryIdentity)
            };
        }

        /// <summary>
        /// Retrieves a <seealso cref="List{T}"/> of <seealso cref="Medication"/> through the business logic objects
        /// for the account specified
        /// </summary>
        /// <param name="request">
        /// The request containing the id of the account to retrieve the prescription pickup for
        /// </param>
        /// <returns>
        /// A message that contains a <seealso cref="List{T}"/> of <seealso cref="Medication"/> that was retrieved
        /// </returns>
        public override Service.MessageContracts.MedicationsMessage GetMedicationsByAccount(Service.MessageContracts.AccountIdPrescriptionRequestMessage request)
        {
            return new Service.MessageContracts.MedicationsMessage
            {
                Medications =
                    medicationManager
                        .GetMedicationsByAccount(
                            request.AccountId,
                            ServiceSecurityContext.Current.PrimaryIdentity)
                        .ToList()
            };
        }

        /// <summary>
        /// Adds a prescription pickup through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the prescription pickup to add
        /// </param>
        public override void AddPrescriptionPickup(Service.MessageContracts.PrescriptionPickupRequestMessage request)
        {
            pickupManager.CreatePrescriptionPickup(request.PrescriptionPickup,  ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Deletes a prescription pickup through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the prescription pickup to delete
        /// </param>
        public override void DeletePrescriptionPickup(Service.MessageContracts.PrescriptionPickupRequestMessage request)
        {
            pickupManager.DeletePrescriptionPickup(request.PrescriptionPickup,  ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Updates a prescription pickup through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the prescription pickup to update
        /// </param>
        public override void UpdatePrescriptionPickup(Service.MessageContracts.PrescriptionPickupRequestMessage request)
        {
            pickupManager.UpdatePrescriptionPickup(request.PrescriptionPickup,  ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Gets a prescription pickup through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the prescription pickup id of the pickup to retrieve
        /// </param>
        /// <returns>
        /// An message containing the requested pickup
        /// </returns>
        public override Service.MessageContracts.PrescriptionPickupMessage GetPrescriptionPickup(Service.MessageContracts.PrescriptionPickupIdMessage request)
        {
            return new Service.MessageContracts.PrescriptionPickupMessage
            {
                PrescriptionPickup =
                    pickupManager
                        .GetPrescriptionPickupById(
                            request.PrescriptionPickupId,
                            ServiceSecurityContext.Current.PrimaryIdentity)
            };
        }

        /// <summary>
        /// Retrieves a <seealso cref="List{T}"/> of <seealso cref="PrescriptionPickup"/> through the business logic objects
        /// for the medication specified
        /// </summary>
        /// <param name="request">
        /// The request containing the id of the medication to retrieve the prescription pickup for
        /// </param>
        /// <returns>
        /// A message that contains a <seealso cref="List{T}"/> of <seealso cref="PrescriptionPickup"/> that was retrieved
        /// </returns>
        public override Service.MessageContracts.PrescriptionPickupsMessage GetPrescriptionPickupsByMedication(Service.MessageContracts.MedicationIdRequestMessage request)
        {
            return new Service.MessageContracts.PrescriptionPickupsMessage
            {
                PrescriptionPickups =
                    pickupManager
                        .GetPrescriptionPickupsByMedication(
                            request.MedicationId,
                            ServiceSecurityContext.Current.PrimaryIdentity)
                        .ToList()
            };
        }

        /// <summary>
        /// Retrieves a <seealso cref="List{T}"/> of <seealso cref="PrescriptionPickup"/> through the business logic objects
        /// for the account specified
        /// </summary>
        /// <param name="request">
        /// The request containing the id of the account to retrieve the prescription pickup for
        /// </param>
        /// <returns>
        /// A message that contains a <seealso cref="List{T}"/> of <seealso cref="PrescriptionPickup"/> that was retrieved
        /// </returns>
        public override Service.MessageContracts.PrescriptionPickupsMessage GetPrescriptionPickupsByAccount(Service.MessageContracts.AccountIdPrescriptionRequestMessage request)
        {
            return new Service.MessageContracts.PrescriptionPickupsMessage
            {
                PrescriptionPickups =
                    pickupManager
                        .GetPrescriptionPickupsByAccount(
                            request.AccountId,
                            ServiceSecurityContext.Current.PrimaryIdentity)
                        .ToList()
            };
        }

        #endregion
    }
}