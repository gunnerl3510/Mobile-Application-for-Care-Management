// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthorizationRequest.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Encapsulates authorization request based data
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Model.Insurance
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;

    /// <summary>
    /// Encapsulates authorization request based data
    /// </summary>
    [DataContract]
    public class AuthorizationRequest : IModel
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
        /// Gets or sets the account identifier to which the medical appointment belongs
        /// </summary>
        [HiddenInput]
        [Editable(false)]
        [DataMember]
        public int AccountId { get; set; }

        /// <summary>
        /// Gets or sets the insurance company identifier to which the authorization request belongs
        /// </summary>
        [Required]
        [DataMember]
        public int InsurerId { get; set; }

        /// <summary>
        /// Gets or sets the description for this authorization request
        /// </summary>
        [Required]
        [StringLength(512, ErrorMessage = "The description of this authorization cannot exceed 512 characters.")]
        [DataMember]
        public string Description { get; set; }
    }
}
