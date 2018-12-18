using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class OpeningLedgerRepository : RepositoryBase<OpeningLedger>, IOpeningLedgerRepository
    {
        public OpeningLedgerRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
      
    }

    public interface IOpeningLedgerRepository : IRepository<OpeningLedger>
    {
       
    }
}
