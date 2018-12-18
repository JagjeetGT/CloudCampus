using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class UnitRepository : RepositoryBase<Unit>, IUnitRepository
    {
        public UnitRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public bool IsProductUnitCodeAvailable(string name)
        {
            var Name = name.ToLower();
            var code = this.GetMany(x => x.Code.ToLower() == Name).Any();
            return !code;
        }
        public bool IsProductUnitNameAvailable(string name)
        {
            var Name = name.ToLower();
            var Description = this.GetMany(x => x.Description.ToLower() == Name).Any();
            return !Description;
        }
    }

    public interface IUnitRepository : IRepository<Unit>
    {
        bool IsProductUnitNameAvailable(string name);
        bool IsProductUnitCodeAvailable(string name);
    }
}
