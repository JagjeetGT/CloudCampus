using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class ScBusRepository : RepositoryBase<ScBus>, IScBusRepository
    {
        public ScBusRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IScBusRepository : IRepository<ScBus>
    {
    }
}
