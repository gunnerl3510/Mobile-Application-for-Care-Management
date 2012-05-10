// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MedicalAppointment.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Encapsulates medical appointment based data
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Model.Medical
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;

    using Infrastructure.Model.Scheduling;

    /// <summary>
    /// Encapsulates medical appointment based data
    /// </summary>
    [DataContract]
    public class MedicalAppointment : IModel, IAppointment, IAppointmentDuration
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
        [DataType(DataType.DateTime)]
        [Display(Name = "Appointment Date and Time")]
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

        /// <summary>
        /// Gets or sets the AppointmentDateTimeUtc using a string value
        /// </summary>
        [Display(Name = "Local Date and Time")]
        [DataType(DataType.DateTime)]
        public string LocalAppointmentDateTimeString
        {
            get
            {
                return AppointmentDateTimeUtc.ToLocalTime().ToString("g");
            }

            set
            {
                DateTime tempDateTime;
                this.AppointmentDateTimeUtc = DateTime.TryParse(value, out tempDateTime)
                                                  ? DateTime.SpecifyKind(tempDateTime, DateTimeKind.Local)
                                                  : default(DateTimeOffset);
            }
        }

        #endregion

        #region Implementation of IAppointmentDuration

        /// <summary>
        /// Gets or sets the units used for the length of the appointment
        /// </summary>
        [Display(Name = "Time Unit")]
        [DataMember]
        public ScheduleUnits? AppointmentLengthUnits { get; set; }

        /// <summary>
        /// Gets or sets the int value of the AppointmentLengthUnits enum value
        /// </summary>
        public int? AppointmentLengthUnitsValue
        {
            get
            {
                return (int?)AppointmentLengthUnits;
            }

            set
            {
                AppointmentLengthUnits = value.HasValue ? (ScheduleUnits)value.Value : (ScheduleUnits?)null;
            }
        }

        /// <summary>
        /// Gets the string value of the AppointmentLengthUnits enum value
        /// </summary>
        [Display(Name = "Time Unit")]
        public string AppointmentLengthUnitsString
        {
            get
            {
                return AppointmentLengthUnits.HasValue ? AppointmentLengthUnits.Value.ToString() : "minutes";
            }
        }

        /// <summary>
        /// Gets or sets the length of the appointment
        /// </summary>
        [DataMember]
        public decimal? Length { get; set; }

        #endregion

        /// <summary>
        /// Gets or sets the account identifier to which the medical appointment belongs
        /// </summary>
        [HiddenInput]
        [Editable(false)]
        [DataMember]
        public int AccountId { get; set; }

        /// <summary>
        /// Gets or sets the provider identifier to which the appointment belongs
        /// </summary>
        [Required]
        [DataMember]
        public int ProviderId { get; set; }

        /// <summary>
        /// Gets or sets the description for the appointment
        /// </summary>
        [StringLength(256, ErrorMessage = "The maximum length for the description is 256 characters.")]
        [DataMember]
        public string Description { get; set; }
    }
}
