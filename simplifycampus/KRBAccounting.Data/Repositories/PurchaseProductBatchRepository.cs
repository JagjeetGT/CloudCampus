using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class PurchaseProductBatchRepository : RepositoryBase<PurchaseProductBatch>, IPurchaseProductBatchRepository
    {
        public PurchaseProductBatchRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IPurchaseProductBatchRepository : IRepository<PurchaseProductBatch>
    {
        
    }
}
