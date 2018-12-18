using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class SalesDetailRepository : RepositoryBase<SalesDetail>, ISalesDetailRepository
    {
        public SalesDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ISalesDetailRepository : IRepository<SalesDetail>
    {
        
    }
}
