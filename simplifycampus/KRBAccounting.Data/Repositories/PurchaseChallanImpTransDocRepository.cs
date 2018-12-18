using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class PurchaseChallanImpTransDocRepository : RepositoryBase<PurchaseChallanImpTransDoc>, IPurchaseChallanImpTransDocRepository
    {
        public PurchaseChallanImpTransDocRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IPurchaseChallanImpTransDocRepository : IRepository<PurchaseChallanImpTransDoc>
    {
    }
}
