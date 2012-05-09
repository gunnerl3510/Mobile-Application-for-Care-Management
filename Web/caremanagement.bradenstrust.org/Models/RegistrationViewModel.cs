// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistrationViewModel.cs" company="LC LLC">
//   All rights reserved
// </copyright>
// <summary>
//   The model used to register a new account
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace caremanagement.bradenstrust.org.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Infrastructure.Model.Account;

    /// <summary>
    /// The model used to register a new account
    /// </summary>
    public class RegistrationViewModel
    {
        /// <summary>
        /// Gets or sets the <seealso cref="Account"/> to register.
        /// </summary>
        public Account Account { get; set; }

        /// <summary>
        /// Gets or sets the password for the account.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirmation password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}