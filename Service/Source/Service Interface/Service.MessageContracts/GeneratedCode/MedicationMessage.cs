
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
	/// Service Contract Class - MedicationMessage
	/// </summary>
	[WCF::MessageContract(IsWrapped = false)] 
	public partial class MedicationMessage
	{
		private Infrastructure.Model.Prescription.Medication medication;
	 		
		[WCF::MessageBodyMember(Namespace = "http://CareManagement.Model/2012/Prescription", Name = "Medication")]
		public Infrastructure.Model.Prescription.Medication Medication
		{
			get { return medication; }
			set { medication = value; }
		}
	}
}
