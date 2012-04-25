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
    using System.Security.Permissions;
    using System.ServiceModel;

    using BusinessLogic.Medical;

    using Ninject;

    /// <summary>
    /// Encapsulates the service logic for working with objects in the Medical domain
    /// </summary>
    public partial class MedicalService
    {
        /// <summary>
        /// The business logic object for manipulating facility objects
        /// </summary>
        private Facilities facilityManager;

        /// <summary>
        /// The business logic object for manipulating provider objects
        /// </summary>
        private Providers providerManager;

        /// <summary>
        /// The business logic object for manipulating provider objects
        /// </summary>
        private MedicalAppointments appointmentsManager;

        /// <summary>
        /// The dependecy injection kernel
        /// </summary>
        private IKernel kernel;

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

        /// <summary>
        /// Prevents a default instance of the <see cref="MedicalService" />
        /// class from being created.
        /// </summary>
        private MedicalService()
        {
        }

        #region MedicalContract Members

        /// <summary>
        /// Adds a facility through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the facility to add
        /// </param>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void AddFacility(Service.MessageContracts.FacilityMessage request)
        {
            facilityManager.CreateFacility(request.Facility, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Deletes a facility through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the facility to delete
        /// </param>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void DeleteFacility(Service.MessageContracts.FacilityMessage request)
        {
            facilityManager.DeleteFacility(request.Facility, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Updates a facility through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the facility to update
        /// </param>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void UpdateFacility(Service.MessageContracts.FacilityMessage request)
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.FacilityMessage GetFacility(Service.MessageContracts.FacilityIdMessage request)
        {
            return new Service.MessageContracts.FacilityMessage
            {
                Facility = facilityManager.GetFacilityById(request.FacilityId, ServiceSecurityContext.Current.PrimaryIdentity)
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.FacilitiesMessage GetFacilitiesByAccount(Service.MessageContracts.AccountIdMessage request)
        {
            return new Service.MessageContracts.FacilitiesMessage
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void AddProvider(Service.MessageContracts.ProviderMessage request)
        {
            providerManager.CreateProvider(request.Provider, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Deletes a provider through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the provider to delete
        /// </param>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void DeleteProvider(Service.MessageContracts.ProviderMessage request)
        {
            providerManager.DeleteProvider(request.Provider, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Updates a provider through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the provider to update
        /// </param>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void UpdateProvider(Service.MessageContracts.ProviderMessage request)
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.ProviderMessage GetProvider(Service.MessageContracts.ProviderIdMessage request)
        {
            return new Service.MessageContracts.ProviderMessage
            {
                Provider = providerManager.GetProviderById(request.ProviderId, ServiceSecurityContext.Current.PrimaryIdentity)
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.ProvidersMessage GetProviderByAccount(Service.MessageContracts.AccountIdMessage request)
        {
            return new Service.MessageContracts.ProvidersMessage
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.ProvidersMessage GetProviderByFacility(Service.MessageContracts.FacilityIdMessage request)
        {
            return new Service.MessageContracts.ProvidersMessage
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void AddMedicalAppointment(Service.MessageContracts.MedicalAppointmentMessage request)
        {
            appointmentsManager.CreateMedicalAppointment(request.MedicalAppointment, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Deletes a medical appointment through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the appointment to delete
        /// </param>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void DeleteMedicalAppointment(Service.MessageContracts.MedicalAppointmentMessage request)
        {
            appointmentsManager.DeleteMedicalAppointment(request.MedicalAppointment, ServiceSecurityContext.Current.PrimaryIdentity);
        }

        /// <summary>
        /// Updates a medical appointment through the business logic objects
        /// </summary>
        /// <param name="request">
        /// The request containing the appointment to update
        /// </param>
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override void UpdateMedicalAppointment(Service.MessageContracts.MedicalAppointmentMessage request)
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.MedicalAppointmentMessage GetMedicalAppointment(Service.MessageContracts.MedicalAppointmentIdMessage request)
        {
            return new Service.MessageContracts.MedicalAppointmentMessage
            {
                MedicalAppointment =
                    appointmentsManager
                        .GetMedicalAppointmentById(request.MedicalAppointmentId, ServiceSecurityContext.Current.PrimaryIdentity)
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.MedicalAppointmentsMessage GetMedicalAppointmentsByAccount(Service.MessageContracts.AccountIdMessage request)
        {
            return new Service.MessageContracts.MedicalAppointmentsMessage
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.MedicalAppointmentsMessage GetMedicalAppointmentsByFacility(Service.MessageContracts.FacilityIdMessage request)
        {
            return new Service.MessageContracts.MedicalAppointmentsMessage
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
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public override Service.MessageContracts.MedicalAppointmentsMessage GetMedicalAppointmentsByProvider(Service.MessageContracts.ProviderIdMessage request)
        {
            return new Service.MessageContracts.MedicalAppointmentsMessage
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
