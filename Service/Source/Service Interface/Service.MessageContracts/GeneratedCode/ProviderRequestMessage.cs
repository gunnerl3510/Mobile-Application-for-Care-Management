
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
	/// Service Contract Class - ProviderRequestMessage
	/// </summary>
	[WCF::MessageContract(IsWrapped = false)] 
	public partial class ProviderRequestMessage
	{
		private Infrastructure.Model.Medical.Provider provider;
	 		
		[WCF::MessageBodyMember(Namespace = "http://CareManagement.Model/2012/Medical", Name = "Provider")]
		public Infrastructure.Model.Medical.Provider Provider
		{
			get { return provider; }
			set { provider = value; }
		}
	}
}

