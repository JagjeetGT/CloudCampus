using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class ModuleListRepository : RepositoryBase<ModuleList>, IModuleListRepository
    {
        public ModuleListRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IModuleListRepository : IRepository<ModuleList>
    {
        
    }
}
