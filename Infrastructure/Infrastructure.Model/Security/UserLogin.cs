// -----------------------------------------------------------------------
// <copyright file="UserLogin.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Infrastructure.Model.Security
{
    using System.Runtime.Serialization;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [DataContract]
    public class UserLogin
    {
        /// <summary>
        /// Gets or sets the user name for the login
        /// </summary>
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password for the login
        /// </summary>
        [DataMember]
        public string Password { get; set; }
    }
}
