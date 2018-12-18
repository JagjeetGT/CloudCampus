using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class PurchaseImpTransDocRepository : RepositoryBase<PurchaseImpTransDoc>, IPurchaseImpTransDocRepository
    {
        public PurchaseImpTransDocRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IPurchaseImpTransDocRepository : IRepository<PurchaseImpTransDoc>
    {
    }
}
