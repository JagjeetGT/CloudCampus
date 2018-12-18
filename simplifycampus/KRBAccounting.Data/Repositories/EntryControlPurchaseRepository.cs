using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class EntryControlPurchaseRepository : RepositoryBase<EntryControlPurchase>, IEntryControlPurchaseRepository
    {
        public EntryControlPurchaseRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IEntryControlPurchaseRepository : IRepository<EntryControlPurchase>
    {
        
    }
}
