using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ProductRepository: RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public bool IsProductItemNameAvailable(string name)
        {
            var Name = name.ToLower();
            var ItemName = this.GetMany(x => x.Name.ToLower() == Name).Any();
            return !ItemName;
        }
        public bool IsProductShortNameAvailable(string name)
        {
            var Name = name.ToLower();
            var ShortName = this.GetMany(x => x.ShortName.ToLower() == Name).Any();
            return !ShortName;
        }
    }

    public interface IProductRepository : IRepository<Product>
    {
        bool IsProductItemNameAvailable(string name);
        bool IsProductShortNameAvailable(string name);

    }
}
