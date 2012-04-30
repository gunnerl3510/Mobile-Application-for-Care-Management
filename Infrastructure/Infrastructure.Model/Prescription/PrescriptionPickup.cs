// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrescriptionPickup.cs" company="LC LLC">
//   All righst reserved LC LLC
// </copyright>
// <summary>
//   Encapsulates prescription pickup appointment based data
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Model.Prescription
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;

    using Infrastructure.Model.Scheduling;

    /// <summary>
    /// Encapsulates prescription pickup appointment based data
    /// </summary>
    [DataContract]
    public class PrescriptionPickup : IModel, IAppointment
    {
        #region Implementation of IModel

        /// <summary>
        /// Gets or sets the unique identifier for this <see cref="IModel"/>
        /// </summary>
        [Key]
        [HiddenInput]
        [Editable(false)]
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the version identifier for the current record
        /// </summary>
        [HiddenInput]
        [Editable(false)]
        [Timestamp]
        [DataMember]
        public byte[] CurrentVersion { get; set; }

        #endregion

        #region Implementation of IAppointment

        /// <summary>
        /// Gets or sets the date and time of the appointment in UTC time
        /// </summary>
        [Required]
        public DateTimeOffset AppointmentDateTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the AppointmentDateTimeUtc using a string value
        /// </summary>
        [ScaffoldColumn(false)]
        [DataMember]
        public string AppointmentDateTimeUtcString
        {
            get
            {
                return AppointmentDateTimeUtc.ToString();
            }

            set
            {
                DateTimeOffset tempDateTime;
                AppointmentDateTimeUtc = DateTimeOffset.TryParse(value, out tempDateTime) ? tempDateTime : default(DateTimeOffset);
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets the account identifier to which the prescription appointment belongs
        /// </summary>
        [HiddenInput]
        [Editable(false)]
        [DataMember]
        public int AccountId { get; set; }

        /// <summary>
        /// Gets or sets the medication identifier to which the prescription belongs
        /// </summary>
        [Required]
        [DataMember]
        public int MedicationId { get; set; }
    }
}
