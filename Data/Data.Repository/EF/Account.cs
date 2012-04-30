// -----------------------------------------------------------------------
// <copyright file="Account.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Data.Repository.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public partial class Account
    {
        public string UserIdString
        {
            get
            {
                return UserId.HasValue ? UserId.Value.ToString() : null;
            }
            set
            {
                Guid tempGuid;
                UserId = Guid.TryParse(value, out tempGuid) ? tempGuid : default(Guid);
            }
        }
    }
}
