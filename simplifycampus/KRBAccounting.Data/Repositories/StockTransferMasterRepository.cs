using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class StockTransferMasterRepository : RepositoryBase<StockTransferMaster>, IStockTransferMasterRepository
    {
        public StockTransferMasterRepository(IDatabaseFactory databaseFactory):base(databaseFactory)
        {
        
        }
    }

    public interface IStockTransferMasterRepository:IRepository<StockTransferMaster>
    {
    
    }
}
