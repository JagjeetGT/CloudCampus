using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ProductGroupRepository: RepositoryBase<ProductGroup>, IProductGroupRepository
    {
        public ProductGroupRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public bool IsProductGroupNameAvailable(string name)
        {
            var Name = name.ToLower();
            var groupName = this.GetMany(x => x.Description.ToLower() == Name).Any();
            return !groupName;
        }
        public bool IsProductGroupShortNameAvailable(string name)
        {
            var Name = name.ToLower();
            var ShortName = this.GetMany(x => x.ShortName.ToLower() == Name).Any();
            return !ShortName;
        }
    }

    public interface IProductGroupRepository : IRepository<ProductGroup>
    {
        bool IsProductGroupShortNameAvailable(string name);
        bool IsProductGroupNameAvailable(string name);
    }
}
