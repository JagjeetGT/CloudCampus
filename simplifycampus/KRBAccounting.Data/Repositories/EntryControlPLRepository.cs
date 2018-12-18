using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class EntryControlPLRepository: RepositoryBase<EntryControlPL>, IEntryControlPLRepository
    {
        public EntryControlPLRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IEntryControlPLRepository : IRepository<EntryControlPL>
    {
        
    }
}
