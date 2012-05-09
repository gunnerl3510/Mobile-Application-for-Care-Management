// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthorizationFollowUp.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Encapsulates authorization deadline based data
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Model.Insurance
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;

    using Infrastructure.Model.Scheduling;

    /// <summary>
    /// Encapsulates authorization follow-up based data
    /// </summary>
    [DataContract]
    public class AuthorizationFollowUp : IModel, IAppointment
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
        [ScaffoldColumn(false)]
        public DateTimeOffset AppointmentDateTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets the AppointmentDateTimeUtc using a string value
        /// </summary>
        [DataMember]
        [ScaffoldColumn(false)]
        public string AppointmentDateTimeUtcString
        {
            get
            {
                return AppointmentDateTimeUtc.ToString("r");
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
                return AppointmentDateTimeUtc.ToLocalTime().ToString("f");
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

        /// <summary>
        /// Gets or sets the account identifier to which the follow up belongs
        /// </summary>
        [HiddenInput]
        [Editable(false)]
        [DataMember]
        public int AccountId { get; set; }

        /// <summary>
        /// Gets or sets the authorization request identifier to which the authorization follow-up belongs
        /// </summary>
        [HiddenInput]
        [Editable(false)]
        [DataMember]
        public int AuthorizationRequestId { get; set; }

        /// <summary>
        /// Gets or sets the description for this follow up
        /// </summary>
        [StringLength(512, ErrorMessage = "The maximum length for the description is 512 characters.")]
        [DataMember]
        public string Description { get; set; }
    }
}
