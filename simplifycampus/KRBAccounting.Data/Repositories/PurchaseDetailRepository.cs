using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class PurchaseDetailRepository : RepositoryBase<PurchaseDetail>, IPurchaseDetailRepository
    {
        public PurchaseDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IPurchaseDetailRepository : IRepository<PurchaseDetail>
    {
        
    }
}
