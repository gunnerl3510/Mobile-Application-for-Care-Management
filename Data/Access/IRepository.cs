// -----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="LC LLC">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Access
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Model;

    /// <summary>
    /// The interface for data repositories for the CareManagement system
    /// </summary>
    public interface IRepository
    {
        User GetUser(int userId);

        int CreateUser(User user);
    }
}
