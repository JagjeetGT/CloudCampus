using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class SalesOrderMasterRepository : RepositoryBase<SalesOrderMaster>, ISalesOrderMasterRepository
    {
        public SalesOrderMasterRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface ISalesOrderMasterRepository : IRepository<SalesOrderMaster>
    {
    }
}
