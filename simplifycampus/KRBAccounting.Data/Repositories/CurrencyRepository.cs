using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class CurrencyRepository : RepositoryBase<Domain.Entities.Currency>, ICurrencyRepository
    {
        public CurrencyRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public bool IsCurrencyNameAvailable(string name)
        {
            var Name = name.ToLower();
            var AreaName = this.GetMany(x => x.Name.ToLower() == Name).Any();
            return !AreaName;
        }
        public bool IsCurrencyCodeAvailable(string name)
        {
            var Name = name.ToLower();
            var Code = this.GetMany(x => x.Code.ToLower() == Name).Any();
            return !Code;
        }
    }

    public interface ICurrencyRepository : IRepository<Domain.Entities.Currency>
    {
        bool IsCurrencyNameAvailable(string name);
        bool IsCurrencyCodeAvailable(string name);
    }
}
