﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthorizationNote.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Encapsulates authorization request note based data
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Model.Insurance
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;

    /// <summary>
    /// Encapsulates authorization request note based data
    /// </summary>
    public class AuthorizationNote : IModel
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
        /// Gets or sets the authorization request that this not belongs to
        /// </summary>
        [Required]
        [DataMember]
        public int AuthorizationRequestId { get; set; }

        /// <summary>
        /// Gets or sets the note text
        /// </summary>
        [Required]
        [StringLength(512, ErrorMessage = "The maximum length for this note is 512 characters.")]
        [DataMember]
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the date and time that the note was created
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        [DataMember]
        public DateTimeOffset Created { get; set; }
    }
}
