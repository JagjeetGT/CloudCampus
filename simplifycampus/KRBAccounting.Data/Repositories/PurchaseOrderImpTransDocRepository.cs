using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class PurchaseOrderImpTransDocRepository : RepositoryBase<PurchaseOrderImpTransDoc>, IPurchaseOrderImpTransDocRepository
    {
        public PurchaseOrderImpTransDocRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IPurchaseOrderImpTransDocRepository : IRepository<PurchaseOrderImpTransDoc>
    {
    }
}
