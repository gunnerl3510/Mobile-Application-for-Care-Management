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
    using System.Web.Mvc;

    using Infrastructure.Model.Scheduling;

    /// <summary>
    /// Encapsulates prescription pickup appointment based data
    /// </summary>
    public class PrescriptionPickup : IModel, IAppointment
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
        public DateTimeOffset AppointmentDateTimeUtc { get; set; }

        #endregion

        /// <summary>
        /// Gets or sets the account identifier to which the prescription appointment belongs
        /// </summary>
        [HiddenInput]
        [Editable(false)]
        public int AccountId { get; set; }

        /// <summary>
        /// Gets or sets the medication identifier to which the prescription belongs
        /// </summary>
        [Required]
        public int MedicationId { get; set; }
    }
}
