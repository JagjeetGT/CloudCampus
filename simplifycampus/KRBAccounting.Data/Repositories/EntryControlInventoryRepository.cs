using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class EntryControlInventoryRepository : RepositoryBase<EntryControlInventory>, IEntryControlInventoryRepository
    {
        public EntryControlInventoryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IEntryControlInventoryRepository : IRepository<EntryControlInventory>
    {
        
    }
}
