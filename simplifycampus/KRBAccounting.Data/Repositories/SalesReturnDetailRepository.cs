using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class SalesReturnDetailRepository : RepositoryBase<SalesReturnDetail>, ISalesReturnDetailRepository
    {
        public SalesReturnDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ISalesReturnDetailRepository : IRepository<SalesReturnDetail>
    {
        
    }
}
