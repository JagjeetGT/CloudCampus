using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ProductSubGroupRepository : RepositoryBase<ProductSubGroup>, IProductSubGroupRepository
    {
        public ProductSubGroupRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public bool IsProductSubGroupNameAvailable(string name)
        {
            var Name = name.ToLower();
            var groupName = this.GetMany(x => x.Description.ToLower() == Name).Any();
            return !groupName;
        }
        
    }

    public interface IProductSubGroupRepository : IRepository<ProductSubGroup>
    {
        bool IsProductSubGroupNameAvailable(string name);
    }
}
