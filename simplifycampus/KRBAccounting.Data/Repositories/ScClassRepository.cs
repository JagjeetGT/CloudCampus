using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class ScClassRepository : RepositoryBase<SchClass>, IScClassRepository
    {
        public ScClassRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IScClassRepository : IRepository<SchClass>
    {
    }
}
