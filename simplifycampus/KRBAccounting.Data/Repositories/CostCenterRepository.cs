using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class CostCenterRepository : RepositoryBase<CostCenter>, ICostCenterRepository
    {
        public CostCenterRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public bool IsCostCenterNameAvailable(string name)
        {
            var Name = name.ToLower();
            var AreaName = this.GetMany(x => x.Name.ToLower() == Name).Any();
            return !AreaName;
        }
        public bool IsCostCenterShorNameAvailable(string name)
        {
            var Name = name.ToLower();
            var Code = this.GetMany(x => x.ShortName.ToLower() == Name).Any();
            return !Code;
        }
    }

    public interface ICostCenterRepository : IRepository<CostCenter>
    {
        bool IsCostCenterNameAvailable(string name);
        bool IsCostCenterShorNameAvailable(string name);
    }
}
