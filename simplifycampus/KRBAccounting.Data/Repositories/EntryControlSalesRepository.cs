using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class EntryControlSalesRepository : RepositoryBase<EntryControlSales>, IEntryControlSalesRepository
    {
        public EntryControlSalesRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IEntryControlSalesRepository : IRepository<EntryControlSales>
    {
        
    }
}
