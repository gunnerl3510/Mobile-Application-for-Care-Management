
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
	/// Service Contract Class - ProviderIdRequestMessage
	/// </summary>
	[WCF::MessageContract(IsWrapped = false)] 
	public partial class ProviderIdRequestMessage
	{
		private int providerId;
	 		
		[WCF::MessageBodyMember(Namespace = "http://CareManagement.Model/2012/Medical", Name = "ProviderId")]
		public int ProviderId
		{
			get { return providerId; }
			set { providerId = value; }
		}
	}
}

