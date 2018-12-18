using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class PurchaseReturnImpTransDocRepository : RepositoryBase<PurchaseReturnImpTransDoc>, IPurchaseReturnImpTransDocRepository
    {
        public PurchaseReturnImpTransDocRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IPurchaseReturnImpTransDocRepository : IRepository<PurchaseReturnImpTransDoc>
    {

    }
}
