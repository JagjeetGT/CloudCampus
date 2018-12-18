using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class SalesChallanImpTransDocRepository : RepositoryBase<SalesChallanImpTransDoc>, ISalesChallanImpTransDocRepository
    {
        public SalesChallanImpTransDocRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface ISalesChallanImpTransDocRepository : IRepository<SalesChallanImpTransDoc>
    {
    }
}
