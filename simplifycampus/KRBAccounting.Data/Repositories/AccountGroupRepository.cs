using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class AccountGroupRepository : RepositoryBase<AccountGroup>, IAccountGroupRepository
    {
        public AccountGroupRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public void UpdateSortOrder(string[] groupIds)
        {
            for (int i = 0; i < groupIds.Length; i++)
            {
                var id = (int.Parse(groupIds[i]));
                var accountGroup = this.GetById(x=>x.Id==id);
                accountGroup.DisplayOrder = i + 1;
                this.Update(accountGroup);
            }
        }
        public bool IsAccountGroupAvailable(string name)
        {
            var Name = name.ToLower();
            var AccountGroup = this.GetMany(x => x.Description.ToLower() == Name).Any();
            return !AccountGroup;
        }
    }

    public interface IAccountGroupRepository : IRepository<AccountGroup>
    {
        void UpdateSortOrder(string[] groupIds);
        bool IsAccountGroupAvailable(string name);
    }
}
