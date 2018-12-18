using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    
    public class LedgerRepository: RepositoryBase<Ledger>, ILedgerRepository
    {
        public static DataContext _context = new DataContext();
        public LedgerRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public  bool IsAccountNameAvailable(string name)
        {
            var Name = name.ToLower();
            var ledger = this.GetMany(x => x.AccountName.ToLower() == Name).Any();
            return !ledger;
        }
        public  bool IsShortNameAvailable(string name)
        {
            var Name = name.Trim().ToLower();
            var ledger = this.GetMany(x => x.ShortName.Trim().ToLower() == Name).Any();
            return !ledger;
        }

    }

    public interface ILedgerRepository : IRepository<Ledger>
    {
        bool IsAccountNameAvailable(string name);
        bool IsShortNameAvailable(string name);
    }
}
