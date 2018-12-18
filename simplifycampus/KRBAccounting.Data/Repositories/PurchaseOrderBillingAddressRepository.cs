using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class PurchaseOrderBillingAddressRepository : RepositoryBase<PurchaseOrderBillingAddress>, IPurchaseOrderBillingAddressRepository
    {
        public PurchaseOrderBillingAddressRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IPurchaseOrderBillingAddressRepository : IRepository<PurchaseOrderBillingAddress>
    {
        
    }
}
