
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
	/// Service Contract Class - InsurerIdMessage
	/// </summary>
	[WCF::MessageContract(IsWrapped = false)] 
	public partial class InsurerIdMessage
	{
		private int insurerId;
	 		
		[WCF::MessageBodyMember(Namespace = "http://CareManagement.Model/2012/Insurance", Name = "InsurerId")]
		public int InsurerId
		{
			get { return insurerId; }
			set { insurerId = value; }
		}
	}
}

