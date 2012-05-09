// -----------------------------------------------------------------------
// <copyright file="Extensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Infrastructure.Security
{
    using System.Security.Principal;
    using System.Web.Security;

    using Infrastructure.Model.Account;
    using Infrastructure.Model.Security;

    /// <summary>
    /// Static class to extend classes to support security operations
    /// </summary>
    public static class Extensions
    {
        public static IIdentity GetIdentity(this UserLogin login)
        {
            return
                Membership.ValidateUser(login.UserName, login.Password)
                    ? new GenericIdentity(login.UserName)
                    : null;
        }
    }
}
