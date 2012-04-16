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
    using System.Web.Mvc;

    /// <summary>
    /// Encapsulates medication based data
    /// </summary>
    public class Medication : IModel
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

        /// <summary>
        /// Gets or sets the account identifier to which the medication belongs
        /// </summary>
        [HiddenInput]
        [Editable(false)]
        public int AccountId { get; set; }

        /// <summary>
        /// Gets or sets the medication name
        /// </summary>
        [Required(ErrorMessage = "A name for the account is required.")]
        [StringLength(256, ErrorMessage = "Account names are restricted to a maximum of 256 characters")]
        public string Name { get; set; }
    }
}
