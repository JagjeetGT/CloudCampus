using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;


namespace KRBAccounting.Data.Repositories
{
    public class StockAdjustmentMasterRepository : RepositoryBase<StockAdjustmentMaster>, IStockAdjustmentMasterRepository
    {
        public StockAdjustmentMasterRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IStockAdjustmentMasterRepository : IRepository<StockAdjustmentMaster>
    {
    }
}
