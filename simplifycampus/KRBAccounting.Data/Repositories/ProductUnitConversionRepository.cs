using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ProductUnitConversionRepository : RepositoryBase<ProductUnitConversion>, IProductUnitConversionRepository
    {
        public ProductUnitConversionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

    }

    public interface IProductUnitConversionRepository : IRepository<ProductUnitConversion>
    {

    }
}
