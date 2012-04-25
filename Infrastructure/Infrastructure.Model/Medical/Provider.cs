﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Provider.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Encapsulates medical provider information
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Model.Medical
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;

    /// <summary>
    /// Encapsulates medical provider information
    /// </summary>
    [DataContract]
    public class Provider : IModel
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
        /// Gets or sets the facility identifier to which the provider belongs
        /// </summary>
        [HiddenInput]
        [Editable(false)]
        [DataMember]
        public int FacilityId { get; set; }

        /// <summary>
        /// Gets or sets the provider's name
        /// </summary>
        [Required(ErrorMessage = "A name for the account is required.")]
        [StringLength(256, ErrorMessage = "Account names are restricted to a maximum of 256 characters")]
        [DataMember]
        public string Name { get; set; }
    }
}
