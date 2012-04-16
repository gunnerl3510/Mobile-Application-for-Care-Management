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
    using System.Web.Mvc;

    using Infrastructure.Model.Scheduling;

    /// <summary>
    /// Encapsulates authorization follow-up based data
    /// </summary>
    public class AuthorizationFollowUp : IModel, IAppointment
    {
        #region Implementation of IModel

        /// <summary>
        /// Gets or sets the unique identifier for this <see cref="IModel"/>
        /// </summary>
        [Key]
        [HiddenInput]
        [Editable(false)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the version identifier for the current record
        /// </summary>
        [HiddenInput]
        [Editable(false)]
        [Timestamp]
        public byte[] CurrentVersion { get; set; }

        #endregion

        #region Implementation of IAppointment

        /// <summary>
        /// Gets or sets the date and time of the appointment in UTC time
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        public DateTimeOffset AppointmentDateTimeUtc { get; set; }

        #endregion

        /// <summary>
        /// Gets or sets the account identifier to which the follow up belongs
        /// </summary>
        [HiddenInput]
        [Editable(false)]
        public int AccountId { get; set; }

        /// <summary>
        /// Gets or sets the authorization request identifier to which the authorization follow-up belongs
        /// </summary>
        [HiddenInput]
        [Editable(false)]
        public int AuthorizationRequestId { get; set; }
    }
}
