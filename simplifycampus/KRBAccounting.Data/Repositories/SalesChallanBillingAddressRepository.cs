using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class SalesChallanBillingAddressRepository : RepositoryBase<SalesChallanBillingAddress>, ISalesChallanBillingAddressRepository
    {
        public SalesChallanBillingAddressRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface ISalesChallanBillingAddressRepository : IRepository<SalesChallanBillingAddress>
    {
    }
}
