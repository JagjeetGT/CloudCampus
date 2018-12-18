using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class SalesOrderDetailRepository : RepositoryBase<SalesOrderDetail>, ISalesOrderDetailRepository
    {
        public SalesOrderDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface ISalesOrderDetailRepository : IRepository<SalesOrderDetail>
    {
    }
}
