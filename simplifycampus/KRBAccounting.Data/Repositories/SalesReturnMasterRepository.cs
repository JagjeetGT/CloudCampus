using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class SalesReturnMasterRepository : RepositoryBase<SalesReturnMaster>, ISalesReturnMasterRepository
    {
        public SalesReturnMasterRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ISalesReturnMasterRepository : IRepository<SalesReturnMaster>
    {
        
    }
}
