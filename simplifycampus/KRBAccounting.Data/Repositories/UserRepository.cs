using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class UserRepository: RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public void AssignRole(int id, List<int> roleId)
        {
            var user = this.GetById(id);
            if (user.Roles != null)
            {
                user.Roles.Clear();
            }
            else
            {
                user.Roles = new List<Role>();
            }
            foreach (int role in roleId)
            {
                var model = this.DataContext.Roles.Find(role);
                user.Roles.Add(model);
            }

            this.DataContext.Users.Attach(user);
            this.DataContext.Entry(user).State = EntityState.Modified;
            this.DataContext.SaveChanges();
        }
    }

    public interface IUserRepository : IRepository<User>
    {
        void AssignRole(int id, List<int> roleId);
    }
}
