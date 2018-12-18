using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class AccountSubGroupRepository : RepositoryBase<AccountSubGroup>, IAccountSubGroupRepository
    {
        public AccountSubGroupRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public bool IsAccountSubGroupAvailable(string name)
        {
            var Name = name.ToLower();
            var AccountGroup = this.GetMany(x => x.Description.ToLower() == Name).Any();
            return !AccountGroup;
        }
    }

    public interface IAccountSubGroupRepository : IRepository<AccountSubGroup>
    {
        bool IsAccountSubGroupAvailable(string name);
    }
}
