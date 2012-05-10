// --------------------------------------------------------------------------------------------------------------------
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
    [DataContract]
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
        /// Gets or sets the account identifier to which the follow up belongs
        /// </summary>
        [HiddenInput]
        [Editable(false)]
        [DataMember]
        public int AccountId { get; set; }

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
        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Gets or sets the Created property using a string
        /// </summary>
        [ScaffoldColumn(false)]
        [DataMember]
        public string CreatedString
        {
            get
            {
                return Created.ToString();
            }

            set
            {
                DateTimeOffset tempDateTime;
                Created = DateTimeOffset.TryParse(value, out tempDateTime) ? tempDateTime : default(DateTimeOffset);
            }
        }

        /// <summary>
        /// Gets the AppointmentDateTimeUtc using a string value
        /// </summary>
        [Display(Name = "Local Date and Time")]
        [DataType(DataType.DateTime)]
        public string LocalCreatedString
        {
            get
            {
                return Created.ToLocalTime().ToString("f");
            }
        }
    }
}
