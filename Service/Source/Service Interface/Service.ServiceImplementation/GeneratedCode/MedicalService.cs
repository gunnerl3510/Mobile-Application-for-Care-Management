//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using WCF = global::System.ServiceModel;

namespace Service.ServiceImplementation
{	
	/// <summary>
	/// Service Class - MedicalService
	/// </summary>
	[WCF::ServiceBehavior(Name = "MedicalService", 
		Namespace = "http://CareManagement.Model/2012/Medical", 
		InstanceContextMode = WCF::InstanceContextMode.PerSession, 
		ConcurrencyMode = WCF::ConcurrencyMode.Single )]
	public abstract class MedicalServiceBase : Service.ServiceContracts.IMedicalContract
	{
		#region MedicalContract Members

		public virtual void AddFacility(Service.MessageContracts.FacilityRequestMessage request)
		{
			
		}

		public virtual void DeleteFacility(Service.MessageContracts.FacilityRequestMessage request)
		{
			
		}

		public virtual void UpdateFacility(Service.MessageContracts.FacilityRequestMessage request)
		{
			
		}

		public virtual Service.MessageContracts.FacilityMessage GetFacility(Service.MessageContracts.FacilityIdRequestMessage request)
		{
			return null;
		}

		public virtual Service.MessageContracts.FacilitiesMessage GetFacilitiesByAccount(Service.MessageContracts.AccountIdMedicalRequestMessage request)
		{
			return null;
		}

		public virtual void AddProvider(Service.MessageContracts.ProviderRequestMessage request)
		{
			
		}

		public virtual void DeleteProvider(Service.MessageContracts.ProviderRequestMessage request)
		{
			
		}

		public virtual void UpdateProvider(Service.MessageContracts.ProviderRequestMessage request)
		{
			
		}

		public virtual Service.MessageContracts.ProviderMessage GetProvider(Service.MessageContracts.ProviderIdRequestMessage request)
		{
			return null;
		}

		public virtual Service.MessageContracts.ProvidersMessage GetProviderByAccount(Service.MessageContracts.AccountIdMedicalRequestMessage request)
		{
			return null;
		}

		public virtual Service.MessageContracts.ProvidersMessage GetProviderByFacility(Service.MessageContracts.FacilityIdRequestMessage request)
		{
			return null;
		}

		public virtual void AddMedicalAppointment(Service.MessageContracts.MedicalAppointmentRequestMessage request)
		{
			
		}

		public virtual void DeleteMedicalAppointment(Service.MessageContracts.MedicalAppointmentRequestMessage request)
		{
			
		}

		public virtual void UpdateMedicalAppointment(Service.MessageContracts.MedicalAppointmentRequestMessage request)
		{
			
		}

		public virtual Service.MessageContracts.MedicalAppointmentMessage GetMedicalAppointment(Service.MessageContracts.MedicalAppointmentIdRequestMessage request)
		{
			return null;
		}

		public virtual Service.MessageContracts.MedicalAppointmentsMessage GetMedicalAppointmentsByAccount(Service.MessageContracts.AccountIdMedicalRequestMessage request)
		{
			return null;
		}

		public virtual Service.MessageContracts.MedicalAppointmentsMessage GetMedicalAppointmentsByFacility(Service.MessageContracts.FacilityIdRequestMessage request)
		{
			return null;
		}

		public virtual Service.MessageContracts.MedicalAppointmentsMessage GetMedicalAppointmentsByProvider(Service.MessageContracts.ProviderIdRequestMessage request)
		{
			return null;
		}

		#endregion		
		
	}
	
	public partial class MedicalService : MedicalServiceBase
	{
	}
	
}

