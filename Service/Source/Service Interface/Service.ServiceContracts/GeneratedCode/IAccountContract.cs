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
	/// Service Contract Class - AccountContract
	/// </summary>
	[WCF::ServiceContract(Namespace = "http://CareManagement.Model/2012/Accounts", Name = "AccountContract", SessionMode = WCF::SessionMode.Allowed, ProtectionLevel = ProtectionLevel.None )]
	public partial interface IAccountContract 
	{
		[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "AddAccount", ReplyAction = "addaccount", ProtectionLevel = ProtectionLevel.None)]
		void AddAccount(Service.MessageContracts.AddAccountMessage request);
		
[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "DeleteAccount", ReplyAction = "deleteaccount", ProtectionLevel = ProtectionLevel.None)]
		void DeleteAccount(Service.MessageContracts.AccountMessage request);
		
[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "UpdateAccount", ReplyAction = "updateaccount", ProtectionLevel = ProtectionLevel.None)]
		void UpdateAccount(Service.MessageContracts.AccountMessage request);
		
[WCF::OperationContract(IsTerminating = false, IsInitiating = true, IsOneWay = false, AsyncPattern = false, Action = "GetAccount", ReplyAction = "getaccount", ProtectionLevel = ProtectionLevel.None)]
		Service.MessageContracts.AccountMessage GetAccount(Service.MessageContracts.AccountIdMessage request);
		
	}
}

