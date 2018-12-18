using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class StockAdjustmentDetailRepository : RepositoryBase<StockAdjustmentDetail>, IStockAdjustmentDetailRepository
    {
        public StockAdjustmentDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IStockAdjustmentDetailRepository : IRepository<StockAdjustmentDetail>
    {
    }
}
