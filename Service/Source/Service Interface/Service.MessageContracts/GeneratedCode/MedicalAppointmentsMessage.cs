
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
	/// Service Contract Class - MedicalAppointmentsMessage
	/// </summary>
	[WCF::MessageContract(IsWrapped = false)] 
	public partial class MedicalAppointmentsMessage
	{
		private System.Collections.Generic.List<Infrastructure.Model.Medical.MedicalAppointment> medicalAppointments;
	 		
		[WCF::MessageBodyMember(Namespace = "http://CareManagement.Model/2012/Medical", Name = "MedicalAppointments")]
		public System.Collections.Generic.List<Infrastructure.Model.Medical.MedicalAppointment> MedicalAppointments
		{
			get { return medicalAppointments; }
			set { medicalAppointments = value; }
		}
	}
}
