using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class SalesOrderAddressRepository : RepositoryBase<SalesOrderAddress>, ISalesOrderAddressRepository
    {
        public SalesOrderAddressRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface ISalesOrderAddressRepository : IRepository<SalesOrderAddress>
    {
    }
}
