
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
	/// Service Contract Class - AuthorizationNoteMessage
	/// </summary>
	[WCF::MessageContract(IsWrapped = false)] 
	public partial class AuthorizationNoteMessage
	{
		private Infrastructure.Model.Insurance.AuthorizationNote authorizationNote;
	 		
		[WCF::MessageBodyMember(Namespace = "http://CareManagement.Model/2012/Insurance", Name = "AuthorizationNote")]
		public Infrastructure.Model.Insurance.AuthorizationNote AuthorizationNote
		{
			get { return authorizationNote; }
			set { authorizationNote = value; }
		}
	}
}

