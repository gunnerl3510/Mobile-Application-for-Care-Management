// -----------------------------------------------------------------------
// <copyright file="EfRepository.cs" company="LC LLC">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Access.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EfRepository : IRepository
    {
        private static EfRepository instance;
        private static readonly CareManagementContainer Container = new CareManagementContainer();

        private EfRepository()
        {
        }

        public EfRepository Instance
        {
            get
            {
                return instance ?? (instance = new EfRepository());
            }
        }

        public Model.User GetUser(int userId)
        {
            return
                Container
                    .Users
                    .Where(user => user.UserId.Equals(userId))
                    .Select(user => new Model.User { UserId = user.UserId, UserName = user.UserName })
                    .SingleOrDefault();
        }

        public int CreateUser(Model.User user)
        {
            var newUser = Container.Users.Add(new User { UserName = user.UserName });
            Container.SaveChanges();

            return newUser.UserId;
        }
    }
}
