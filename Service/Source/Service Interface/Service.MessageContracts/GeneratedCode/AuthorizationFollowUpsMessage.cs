
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
	/// Service Contract Class - AuthorizationFollowUpsMessage
	/// </summary>
	[WCF::MessageContract(IsWrapped = false)] 
	public partial class AuthorizationFollowUpsMessage
	{
		private System.Collections.Generic.List<Infrastructure.Model.Insurance.AuthorizationFollowUp> authorizationFollowUps;
	 		
		[WCF::MessageBodyMember(Namespace = "http://CareManagement.Model/2012/Insurance", Name = "AuthorizationFollowUps")]
		public System.Collections.Generic.List<Infrastructure.Model.Insurance.AuthorizationFollowUp> AuthorizationFollowUps
		{
			get { return authorizationFollowUps; }
			set { authorizationFollowUps = value; }
		}
	}
}

