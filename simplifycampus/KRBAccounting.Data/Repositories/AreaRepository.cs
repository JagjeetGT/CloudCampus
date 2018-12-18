using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class AreaRepository: RepositoryBase<Area>, IAreaRepository
    {
        public AreaRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public bool IsAreaNameAvailable(string name)
        {
            var Name = name.ToLower();
            var AreaName = this.GetMany(x => x.AreaName.ToLower() == Name).Any();
            return !AreaName;
        }
        public bool IsAreaShortNameAvailable(string name)
        {
            var Name = name.ToLower();
            var AreaName = this.GetMany(x => x.ShorName.ToLower() == Name).Any();
            return !AreaName;
        }
    }

    public interface IAreaRepository : IRepository<Area>
    {
        bool IsAreaNameAvailable(string name);
        bool IsAreaShortNameAvailable(string name);
    }
}
