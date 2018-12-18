using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class StockTransactionRepository : RepositoryBase<StockTransaction>, IStockTransactionRepository
    {
        public StockTransactionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IStockTransactionRepository : IRepository<StockTransaction>
    {
        
    }
}
