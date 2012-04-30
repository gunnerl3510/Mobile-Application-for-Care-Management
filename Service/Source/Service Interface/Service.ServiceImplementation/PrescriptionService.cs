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
            medicationManager.CreateMedication(request.Medication,  request.User.GetIdentity());
        }

        /// <summary>
        /// Deletes a medication through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the medication to delete
        /// </param>
        public override void DeleteMedication(Service.MessageContracts.MedicationRequestMessage request)
        {
            medicationManager.DeleteMedication(request.Medication,  request.User.GetIdentity());
        }

        /// <summary>
        /// Updates a medication through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the medication to update
        /// </param>
        public override void UpdateMedication(Service.MessageContracts.MedicationRequestMessage request)
        {
            medicationManager.UpdateMedication(request.Medication,  request.User.GetIdentity());
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
                            request.User.GetIdentity())
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
        public override Service.MessageContracts.MedicationsMessage GetMedicationsByAccount(Service.MessageContracts.AccountIdRequestMessage request)
        {
            return new Service.MessageContracts.MedicationsMessage
            {
                Medications =
                    medicationManager
                        .GetMedicationsByAccount(
                            request.AccountId,
                            request.User.GetIdentity())
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
            pickupManager.CreatePrescriptionPickup(request.PrescriptionPickup,  request.User.GetIdentity());
        }

        /// <summary>
        /// Deletes a prescription pickup through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the prescription pickup to delete
        /// </param>
        public override void DeletePrescriptionPickup(Service.MessageContracts.PrescriptionPickupRequestMessage request)
        {
            pickupManager.DeletePrescriptionPickup(request.PrescriptionPickup,  request.User.GetIdentity());
        }

        /// <summary>
        /// Updates a prescription pickup through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the prescription pickup to update
        /// </param>
        public override void UpdatePrescriptionPickup(Service.MessageContracts.PrescriptionPickupRequestMessage request)
        {
            pickupManager.UpdatePrescriptionPickup(request.PrescriptionPickup,  request.User.GetIdentity());
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
                            request.User.GetIdentity())
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
                            request.User.GetIdentity())
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
        public override Service.MessageContracts.PrescriptionPickupsMessage GetPrescriptionPickupsByAccount(Service.MessageContracts.AccountIdRequestMessage request)
        {
            return new Service.MessageContracts.PrescriptionPickupsMessage
            {
                PrescriptionPickups =
                    pickupManager
                        .GetPrescriptionPickupsByAccount(
                            request.AccountId,
                            request.User.GetIdentity())
                        .ToList()
            };
        }

        #endregion
    }
}