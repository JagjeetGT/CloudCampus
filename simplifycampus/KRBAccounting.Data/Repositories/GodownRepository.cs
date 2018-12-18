using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class GodownRepository: RepositoryBase<Godown>, IGodownRepository
    {
        public GodownRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public bool IsGodownNameAvailable(string name)
        {
            var Name = name.ToLower();
            var AreaName = this.GetMany(x => x.Name.ToLower() == Name).Any();
            return !AreaName;
        }
        public bool IsGodownShortNameAvailable(string name)
        {
            var Name = name.ToLower();
            var ShortName = this.GetMany(x => x.ShortName.ToLower() == Name).Any();
            return !ShortName;
        }
    }

    public interface IGodownRepository : IRepository<Godown>
    {
       bool IsGodownShortNameAvailable(string name);
        bool IsGodownNameAvailable(string name);
    }
}
