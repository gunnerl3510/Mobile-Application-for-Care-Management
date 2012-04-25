// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Account.cs" company="LC LLC">
//   copyright LC LLC 2012
// </copyright>
// <summary>
//   Encapsulates account based data
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Model.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;

    /// <summary>
    /// Encapsulates account based data
    /// </summary>
    [DataContract]
    public class Account : IModel
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
        /// Gets or sets the unique account name
        /// </summary>
        [Required(ErrorMessage = "A name for the account is required.")]
        [StringLength(256, ErrorMessage = "Account names are restricted to a maximum of 256 characters")]
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address for the account
        /// </summary>
        [Required(ErrorMessage = "An email address is required for the account.")]
        [StringLength(100, ErrorMessage = "Account email addresses are restricted to a maximum of 100 characters")]
        [DataMember]
        public string EmailAddress { get; set; }

        private Guid userId;

        /// <summary>
        /// Gets or sets the unique id for the user's relationship to the
        /// membership provider.
        /// </summary>
        [ScaffoldColumn(false)]
        [Editable(false)]
        [DataMember]
        public string UserId
        {
            get { return userId.ToString(); }
            set
            {
                if (!Guid.TryParse(value, out userId))
                {
                    userId = default(Guid);
                }
            }
        }        
    }
}
