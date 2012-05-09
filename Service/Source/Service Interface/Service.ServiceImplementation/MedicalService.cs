// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MedicalService.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Encapsulates the service logic for working with objects in the Medical domain
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Service.ServiceImplementation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;

    using BusinessLogic.Medical;

    using Infrastructure.Model.Medical;

    using Ninject;

    /// <summary>
    /// Encapsulates the service logic for working with objects in the Medical domain
    /// </summary>
    public partial class MedicalService
    {
        /// <summary>
        /// The business logic object for manipulating facility objects
        /// </summary>
        private readonly Facilities facilityManager;

        /// <summary>
        /// The business logic object for manipulating provider objects
        /// </summary>
        private readonly Providers providerManager;

        /// <summary>
        /// The business logic object for manipulating provider objects
        /// </summary>
        private readonly MedicalAppointments appointmentsManager;

        /// <summary>
        /// The dependecy injection kernel
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicalService" />
        /// class
        /// </summary>
        /// <param name="kernel">
        /// The dependency injection object
        /// </param>
        public MedicalService(IKernel kernel)
        {
            this.kernel = kernel;
            facilityManager = this.kernel.Get<Facilities>();
            providerManager = this.kernel.Get<Providers>();
            appointmentsManager = this.kernel.Get<MedicalAppointments>();
        }

        #region MedicalContract Members

        /// <summary>
        /// Adds a facility through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the facility to add
        /// </param>
        public override void AddFacility(MessageContracts.FacilityRequestMessage request)
        {
            facilityManager.CreateFacility(request.Facility, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Deletes a facility through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the facility to delete
        /// </param>
        public override void DeleteFacility(MessageContracts.FacilityRequestMessage request)
        {
            facilityManager.DeleteFacility(request.Facility, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Updates a facility through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the facility to update
        /// </param>
        public override void UpdateFacility(MessageContracts.FacilityRequestMessage request)
        {
            facilityManager.UpdateFacility(request.Facility, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Gets a facility through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the facility id of the facility to retrieve
        /// </param>
        /// <returns>
        /// An message containing the requested facility
        /// </returns>
        public override MessageContracts.FacilityMessage GetFacility(MessageContracts.FacilityIdRequestMessage request)
        {
            return new MessageContracts.FacilityMessage
            {
                Facility = facilityManager.GetFacilityById(request.FacilityId,  ServiceSecurityContext.Current.PrimaryIdentity)
            };
        }

        /// <summary>
        /// Retrieves a <seealso cref="List{T}"/> of <seealso cref="Facility"/> through the business logic objects
        /// for the account specified
        /// </summary>
        /// <param name="request">
        /// The request containing the id of the account to retrieve the facilities for
        /// </param>
        /// <returns>
        /// A message that contains a <seealso cref="List{T}"/> of <seealso cref="Facility"/> that was retrieved
        /// </returns>
        public override MessageContracts.FacilitiesMessage GetFacilitiesByAccount(MessageContracts.AccountIdMedicalRequestMessage request)
        {
            return new MessageContracts.FacilitiesMessage
            {
                Facilities =
                    facilityManager
                        .GetFacilitiesByAccount(
                            request.AccountId,
                             ServiceSecurityContext.Current.PrimaryIdentity)
                        .ToList()
            };
        }

        /// <summary>
        /// Adds a provider through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the provider to add
        /// </param>
        public override void AddProvider(MessageContracts.ProviderRequestMessage request)
        {
            providerManager.CreateProvider(request.Provider, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Deletes a provider through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the provider to delete
        /// </param>
        public override void DeleteProvider(MessageContracts.ProviderRequestMessage request)
        {
            providerManager.DeleteProvider(request.Provider, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Updates a provider through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the provider to update
        /// </param>
        public override void UpdateProvider(MessageContracts.ProviderRequestMessage request)
        {
            providerManager.UpdateProvider(request.Provider, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Gets an provider through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the provider id of the provider to retrieve
        /// </param>
        /// <returns>
        /// An message containing the requested provider
        /// </returns>
        public override MessageContracts.ProviderMessage GetProvider(MessageContracts.ProviderIdRequestMessage request)
        {
            return new MessageContracts.ProviderMessage
            {
                Provider = providerManager.GetProviderById(request.ProviderId,  ServiceSecurityContext.Current.PrimaryIdentity)
            };
        }

        /// <summary>
        /// Retrieves a <seealso cref="List{T}"/> of <seealso cref="Provider"/> through the business logic objects
        /// for the account specified
        /// </summary>
        /// <param name="request">
        /// The request containing the id of the account to retrieve the providers for
        /// </param>
        /// <returns>
        /// A message that contains a <seealso cref="List{T}"/> of <seealso cref="Provider"/> that was retrieved
        /// </returns>
        public override MessageContracts.ProvidersMessage GetProviderByAccount(MessageContracts.AccountIdMedicalRequestMessage request)
        {
            return new MessageContracts.ProvidersMessage
            {
                Providers =
                    providerManager
                        .GetProvidersByAccount(
                            request.AccountId,
                             ServiceSecurityContext.Current.PrimaryIdentity)
                        .ToList()
            };
        }

        /// <summary>
        /// Retrieves a <seealso cref="List{T}"/> of <seealso cref="Provider"/> through the business logic objects
        /// for the facility specified
        /// </summary>
        /// <param name="request">
        /// The request containing the id of the facility to retrieve the providers for
        /// </param>
        /// <returns>
        /// A message that contains a <seealso cref="List{T}"/> of <seealso cref="Provider"/> that was retrieved
        /// </returns>
        public override MessageContracts.ProvidersMessage GetProviderByFacility(MessageContracts.FacilityIdRequestMessage request)
        {
            return new MessageContracts.ProvidersMessage
            {
                Providers =
                    providerManager
                        .GetProvidersByFacility(
                            request.FacilityId,
                             ServiceSecurityContext.Current.PrimaryIdentity)
                        .ToList()
            };
        }

        /// <summary>
        /// Adds a medical appointment through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the appointment to add
        /// </param>
        public override void AddMedicalAppointment(MessageContracts.MedicalAppointmentRequestMessage request)
        {
            appointmentsManager.CreateMedicalAppointment(request.MedicalAppointment, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Deletes a medical appointment through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the appointment to delete
        /// </param>
        public override void DeleteMedicalAppointment(MessageContracts.MedicalAppointmentRequestMessage request)
        {
            appointmentsManager.DeleteMedicalAppointment(request.MedicalAppointment, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Updates a medical appointment through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the appointment to update
        /// </param>
        public override void UpdateMedicalAppointment(MessageContracts.MedicalAppointmentRequestMessage request)
        {
            appointmentsManager.UpdateMedicalAppointment(request.MedicalAppointment, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Gets a medical appointment through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the medical appointment id of the appointent to retrieve
        /// </param>
        /// <returns>
        /// An message containing the requested appointment
        /// </returns>
        public override MessageContracts.MedicalAppointmentMessage GetMedicalAppointment(MessageContracts.MedicalAppointmentIdRequestMessage request)
        {
            return new MessageContracts.MedicalAppointmentMessage
            {
                MedicalAppointment =
                    appointmentsManager
                        .GetMedicalAppointmentById(request.MedicalAppointmentId,  ServiceSecurityContext.Current.PrimaryIdentity)
            };
        }

        /// <summary>
        /// Retrieves a <seealso cref="List{T}"/> of <seealso cref="MedicalAppointment"/> through the business logic objects
        /// for the account specified
        /// </summary>
        /// <param name="request">
        /// The request containing the id of the account to retrieve the appointments for
        /// </param>
        /// <returns>
        /// A message that contains a <seealso cref="List{T}"/> of <seealso cref="MedicalAppointment"/> that was retrieved
        /// </returns>
        public override MessageContracts.MedicalAppointmentsMessage GetMedicalAppointmentsByAccount(MessageContracts.AccountIdMedicalRequestMessage request)
        {
            return new MessageContracts.MedicalAppointmentsMessage
            {
                MedicalAppointments =
                    appointmentsManager
                        .GetMedicalAppointmentsByAccount(
                            request.AccountId,
                             ServiceSecurityContext.Current.PrimaryIdentity)
                        .ToList()
            };
        }

        /// <summary>
        /// Retrieves a <seealso cref="List{T}"/> of <seealso cref="MedicalAppointment"/> through the business logic objects
        /// for the facility specified
        /// </summary>
        /// <param name="request">
        /// The request containing the id of the facility to retrieve the appointments for
        /// </param>
        /// <returns>
        /// A message that contains a <seealso cref="List{T}"/> of <seealso cref="MedicalAppointment"/> that was retrieved
        /// </returns>
        public override MessageContracts.MedicalAppointmentsMessage GetMedicalAppointmentsByFacility(MessageContracts.FacilityIdRequestMessage request)
        {
            return new MessageContracts.MedicalAppointmentsMessage
            {
                MedicalAppointments =
                    appointmentsManager
                        .GetMedicalAppointmentsByFacility(
                            request.FacilityId,
                             ServiceSecurityContext.Current.PrimaryIdentity)
                        .ToList()
            };
        }

        /// <summary>
        /// Retrieves a <seealso cref="List{T}"/> of <seealso cref="MedicalAppointment"/> through the business logic objects
        /// for the provider specified
        /// </summary>
        /// <param name="request">
        /// The request containing the id of the provider to retrieve the appointments for
        /// </param>
        /// <returns>
        /// A message that contains a <seealso cref="List{T}"/> of <seealso cref="MedicalAppointment"/> that was retrieved
        /// </returns>
        public override MessageContracts.MedicalAppointmentsMessage GetMedicalAppointmentsByProvider(MessageContracts.ProviderIdRequestMessage request)
        {
            return new MessageContracts.MedicalAppointmentsMessage
            {
                MedicalAppointments =
                    appointmentsManager
                        .GetMedicalAppointmentsByProvider(
                            request.ProviderId,
                             ServiceSecurityContext.Current.PrimaryIdentity)
                        .ToList()
            };
        }

        #endregion
    }
}
