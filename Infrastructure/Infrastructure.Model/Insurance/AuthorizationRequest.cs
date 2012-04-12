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
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    /// <summary>
    /// Encapsulates authorization request based data
    /// </summary>
    public class AuthorizationRequest : IModel
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
        /// Gets or sets the account identifier to which the medical appointment belongs
        /// </summary>
        [HiddenInput]
        [Editable(false)]
        public int AccountId { get; set; }

        /// <summary>
        /// Gets or sets the insurance company identifier to which the authorization request belongs
        /// </summary>
        [Required]
        public int InsurerId { get; set; }
    }
}
