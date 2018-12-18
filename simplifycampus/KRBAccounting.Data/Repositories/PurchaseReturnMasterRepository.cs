using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class PurchaseReturnMasterRepository : RepositoryBase<PurchaseReturnMaster>, IPurchaseReturnMasterRepository
    {
        public PurchaseReturnMasterRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IPurchaseReturnMasterRepository : IRepository<PurchaseReturnMaster>
    {
        
    }
}
