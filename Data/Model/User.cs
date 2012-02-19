// -----------------------------------------------------------------------
// <copyright file="User.cs" company="LC LLC">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The properties that define a User
    /// </summary>
    public class User
    {
        [Key]
        [Required]
        public int UserId { get; set; }

        [StringLength(20, ErrorMessage = "The maximum user name length is 20 characters")]
        [Required]
        public string UserName { get; set; }
    }
}
