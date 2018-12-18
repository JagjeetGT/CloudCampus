using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class SubLedgerRepository: RepositoryBase<SubLedger>, ISubLedgerRepository
    {
        public SubLedgerRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public bool IsSubAccountNameAvailable(string name)
        {
            var Name = name.ToLower();
            var Subledger = this.GetMany(x => x.Description.ToLower() == Name).Any();
            return !Subledger;
        }
        public bool IsSubShortNameAvailable(string name)
        {
            var Name = name.Trim().ToLower();
            var Subledger = this.GetMany(x => x.ShortName.Trim().ToLower() == Name).Any();
            return !Subledger;
        }
    }

    public interface ISubLedgerRepository : IRepository<SubLedger>
    {
        bool IsSubAccountNameAvailable(string name);
        bool IsSubShortNameAvailable(string name);
    }
}
