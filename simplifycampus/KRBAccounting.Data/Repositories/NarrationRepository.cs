using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class NarrationRepository: RepositoryBase<Narration>, INarrationRepository
    {
        public NarrationRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        public bool IsNarrationNameAvailable(string name)
        {
            var Name = name.ToLower();
            var AreaName = this.GetMany(x => x.Name.ToLower() == Name).Any();
            return !AreaName;
        }
    }

    public interface INarrationRepository : IRepository<Narration>
    {
        bool IsNarrationNameAvailable(string name);
    }
}
