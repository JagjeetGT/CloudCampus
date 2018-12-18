using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class SalesChallanDetailRepository : RepositoryBase<SalesChallanDetail>, ISalesChallanDetailRepository
    {
        public SalesChallanDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface ISalesChallanDetailRepository : IRepository<SalesChallanDetail>
    {
    }
}
