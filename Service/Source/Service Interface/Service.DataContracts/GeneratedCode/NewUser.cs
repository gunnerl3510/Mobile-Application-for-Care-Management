//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using WcfSerialization = global::System.Runtime.Serialization;

namespace Service.DataContracts
{
	/// <summary>
	/// Data Contract Class - NewUser
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://CareManagement.Model/2012/User", Name = "NewUser")]
	public partial class NewUser 
	{
		private string userName;
		
		[WcfSerialization::DataMember(Name = "UserName", IsRequired = false, Order = 0)]
		public string UserName
		{
		  get { return userName; }
		  set { userName = value; }
		}				
	}
}

