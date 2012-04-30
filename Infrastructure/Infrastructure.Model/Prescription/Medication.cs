// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Medication.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Encapsulates medication based data
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Model.Prescription
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;

    /// <summary>
    /// Encapsulates medication based data
    /// </summary>
    [DataContract]
    public class Medication : IModel
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

        /// <summary>
        /// Gets or sets the account identifier to which the medication belongs
        /// </summary>
        [HiddenInput]
        [Editable(false)]
        [DataMember]
        public int AccountId { get; set; }

        /// <summary>
        /// Gets or sets the medication name
        /// </summary>
        [Required(ErrorMessage = "A name for the account is required.")]
        [StringLength(256, ErrorMessage = "Account names are restricted to a maximum of 256 characters")]
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the units for the dosage for the medication
        /// </summary>
        [DataMember]
        public DosageUnits? DosageUnits { get; set; }

        /// <summary>
        /// Gets or sets the int value of the AppointmentLengthUnits enum value
        /// </summary>
        public int? DosageUnitsValue
        {
            get
            {
                return (int?)DosageUnits;
            }

            set
            {
                DosageUnits = value.HasValue ? (DosageUnits)value.Value : (DosageUnits?)null;
            }
        }

        /// <summary>
        /// Gets or sets the quantity per dose for the medication
        /// </summary>
        [DataMember]
        public decimal? Quantity { get; set; }
    }
}
