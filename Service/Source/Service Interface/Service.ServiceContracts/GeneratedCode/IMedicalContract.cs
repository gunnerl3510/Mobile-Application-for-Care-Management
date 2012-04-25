//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Net.Security;
using WCF = global::System.ServiceModel;

namespace Service.ServiceContracts
{
	/// <summary>
	/// Service Contract Class - MedicalContract
	/// </summary>
	[WCF::ServiceContract(Namespace = "http://CareManagement.Model/2012/Medical", Name = "MedicalContract", SessionMode = WCF::SessionMode.Allowed, ProtectionLevel = ProtectionLevel.None )]
	public partial interface IMedicalContract 
	{
		[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://CareManagement.Model/2012/Medical/MedicalContract/AddFacility", ReplyAction = "addfacility", ProtectionLevel = ProtectionLevel.None)]
		void AddFacility(Service.MessageContracts.FacilityMessage request);
		
[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://CareManagement.Model/2012/Medical/MedicalContract/DeleteFacility", ReplyAction = "deletefacility", ProtectionLevel = ProtectionLevel.None)]
		void DeleteFacility(Service.MessageContracts.FacilityMessage request);
		
[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://CareManagement.Model/2012/Medical/MedicalContract/UpdateFacility", ReplyAction = "updatefacility", ProtectionLevel = ProtectionLevel.None)]
		void UpdateFacility(Service.MessageContracts.FacilityMessage request);
		
[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "http://CareManagement.Model/2012/Medical/MedicalContract/GetFacility", ReplyAction = "getfacility", ProtectionLevel = ProtectionLevel.None)]
		Service.MessageContracts.FacilityMessage GetFacility(Service.MessageContracts.FacilityIdMessage request);
		
[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "GetFacilitiesByAccount", ReplyAction = "getfacilitiesbyaccount", ProtectionLevel = ProtectionLevel.None)]
		Service.MessageContracts.FacilitiesMessage GetFacilitiesByAccount(Service.MessageContracts.AccountIdMessage request);
		
[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "AddProvider", ReplyAction = "addprovider", ProtectionLevel = ProtectionLevel.None)]
		void AddProvider(Service.MessageContracts.ProviderMessage request);
		
[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "DeleteProvider", ReplyAction = "deleteprovider", ProtectionLevel = ProtectionLevel.None)]
		void DeleteProvider(Service.MessageContracts.ProviderMessage request);
		
[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "UpdateProvider", ReplyAction = "updateprovider", ProtectionLevel = ProtectionLevel.None)]
		void UpdateProvider(Service.MessageContracts.ProviderMessage request);
		
[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "GetProvider", ReplyAction = "getprovider", ProtectionLevel = ProtectionLevel.None)]
		Service.MessageContracts.ProviderMessage GetProvider(Service.MessageContracts.ProviderIdMessage request);
		
[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "GetProviderByAccount", ReplyAction = "getproviderbyaccount", ProtectionLevel = ProtectionLevel.None)]
		Service.MessageContracts.ProvidersMessage GetProviderByAccount(Service.MessageContracts.AccountIdMessage request);
		
[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "GetProviderByFacility", ReplyAction = "getproviderbyfacility", ProtectionLevel = ProtectionLevel.None)]
		Service.MessageContracts.ProvidersMessage GetProviderByFacility(Service.MessageContracts.FacilityIdMessage request);
		
[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "AddMedicalAppointment", ReplyAction = "addmedicalappointment", ProtectionLevel = ProtectionLevel.None)]
		void AddMedicalAppointment(Service.MessageContracts.MedicalAppointmentMessage request);
		
[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "DeleteMedicalAppointment", ReplyAction = "deletemedicalappointment", ProtectionLevel = ProtectionLevel.None)]
		void DeleteMedicalAppointment(Service.MessageContracts.MedicalAppointmentMessage request);
		
[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "UpdateMedicalAppointment", ReplyAction = "updatemedicalappointment", ProtectionLevel = ProtectionLevel.None)]
		void UpdateMedicalAppointment(Service.MessageContracts.MedicalAppointmentMessage request);
		
[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "GetMedicalAppointment", ReplyAction = "getmedicalappointment", ProtectionLevel = ProtectionLevel.None)]
		Service.MessageContracts.MedicalAppointmentMessage GetMedicalAppointment(Service.MessageContracts.MedicalAppointmentIdMessage request);
		
[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "GetMedicalAppointmentsByAccount", ReplyAction = "GetMedicalAppointmentsByAccount", ProtectionLevel = ProtectionLevel.None)]
		Service.MessageContracts.MedicalAppointmentsMessage GetMedicalAppointmentsByAccount(Service.MessageContracts.AccountIdMessage request);
		
[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "GetMedicalAppointmentsByFacility", ReplyAction = "getmedicalappointmentsbyfacility", ProtectionLevel = ProtectionLevel.None)]
		Service.MessageContracts.MedicalAppointmentsMessage GetMedicalAppointmentsByFacility(Service.MessageContracts.FacilityIdMessage request);
		
[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "GetMedicalAppointmentsByProvider", ReplyAction = "getmedicalappointmentsbyprovider", ProtectionLevel = ProtectionLevel.None)]
		Service.MessageContracts.MedicalAppointmentsMessage GetMedicalAppointmentsByProvider(Service.MessageContracts.ProviderIdMessage request);
		
	}
}

