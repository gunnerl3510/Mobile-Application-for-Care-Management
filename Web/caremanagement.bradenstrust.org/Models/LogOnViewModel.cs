// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogOnViewModel.cs" company="LC LLC">
//   All rights reserved
// </copyright>
// <summary>
//   The view model that is used to log a user into the system.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace caremanagement.bradenstrust.org.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The view model that is used to log a user into the system.
    /// </summary>
    public class LogOnViewModel
    {
        /// <summary>
        /// Gets or sets UserName.
        /// </summary>
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the authorization cookie should persist.
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
