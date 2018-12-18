using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class StockTransferDetailRepository:RepositoryBase<StockTransferDetail>,IStockTransferDetailRepository
    {
        public StockTransferDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        { 
        
        }
    }

    public interface IStockTransferDetailRepository : IRepository<StockTransferDetail>
    { 
    
    }
}
