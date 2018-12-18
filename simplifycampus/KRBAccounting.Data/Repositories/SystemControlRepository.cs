using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class SystemControlRepository : RepositoryBase<SystemControl>, ISystemControlRepository
    {
        public SystemControlRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ISystemControlRepository : IRepository<SystemControl>
    {
        
    }
}
