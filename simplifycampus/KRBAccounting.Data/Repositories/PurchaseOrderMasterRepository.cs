using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class PurchaseOrderMasterRepository : RepositoryBase<PurchaseOrderMaster>, IPurchaseOrderMasterRepository
    {
        public PurchaseOrderMasterRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IPurchaseOrderMasterRepository : IRepository<PurchaseOrderMaster>
    {
        
    }
}
