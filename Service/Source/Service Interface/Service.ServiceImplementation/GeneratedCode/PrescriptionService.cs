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
	/// Service Class - PrescriptionService
	/// </summary>
	[WCF::ServiceBehavior(Name = "PrescriptionService", 
		Namespace = "http://CareManagement.Model/2012/Prescription", 
		InstanceContextMode = WCF::InstanceContextMode.PerSession, 
		ConcurrencyMode = WCF::ConcurrencyMode.Single )]
	public abstract class PrescriptionServiceBase : Service.ServiceContracts.IPrescriptionContract
	{
		#region PrescriptionContract Members

		public virtual void AddMedication(Service.MessageContracts.MedicationRequestMessage request)
		{
			
		}

		public virtual void DeleteMedication(Service.MessageContracts.MedicationRequestMessage request)
		{
			
		}

		public virtual void UpdateMedication(Service.MessageContracts.MedicationRequestMessage request)
		{
			
		}

		public virtual Service.MessageContracts.MedicationMessage GetMedication(Service.MessageContracts.MedicationIdRequestMessage request)
		{
			return null;
		}

		public virtual Service.MessageContracts.MedicationsMessage GetMedicationsByAccount(Service.MessageContracts.AccountIdRequestMessage request)
		{
			return null;
		}

		public virtual void AddPrescriptionPickup(Service.MessageContracts.PrescriptionPickupRequestMessage request)
		{
			
		}

		public virtual void DeletePrescriptionPickup(Service.MessageContracts.PrescriptionPickupRequestMessage request)
		{
			
		}

		public virtual void UpdatePrescriptionPickup(Service.MessageContracts.PrescriptionPickupRequestMessage request)
		{
			
		}

		public virtual Service.MessageContracts.PrescriptionPickupMessage GetPrescriptionPickup(Service.MessageContracts.PrescriptionPickupIdMessage request)
		{
			return null;
		}

		public virtual Service.MessageContracts.PrescriptionPickupsMessage GetPrescriptionPickupsByMedication(Service.MessageContracts.MedicationIdRequestMessage request)
		{
			return null;
		}

		public virtual Service.MessageContracts.PrescriptionPickupsMessage GetPrescriptionPickupsByAccount(Service.MessageContracts.AccountIdRequestMessage request)
		{
			return null;
		}

		#endregion		
		
	}
	
	public partial class PrescriptionService : PrescriptionServiceBase
	{
	}
	
}
