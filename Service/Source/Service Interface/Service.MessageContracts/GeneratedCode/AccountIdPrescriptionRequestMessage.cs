
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

namespace Service.MessageContracts
{
	/// <summary>
	/// Service Contract Class - AccountIdPrescriptionRequestMessage
	/// </summary>
	[WCF::MessageContract(IsWrapped = false)] 
	public partial class AccountIdPrescriptionRequestMessage
	{
		private int accountId;
	 		
		[WCF::MessageBodyMember(Namespace = "http://CareManagement.Model/2012/Prescription", Name = "AccountId")]
		public int AccountId
		{
			get { return accountId; }
			set { accountId = value; }
		}
	}
}

