using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class PurchaseReturnDetailRepository : RepositoryBase<PurchaseReturnDetail>, IPurchaseReturnDetailRepository
    {
        public PurchaseReturnDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IPurchaseReturnDetailRepository : IRepository<PurchaseReturnDetail>
    {
        
    }
}
