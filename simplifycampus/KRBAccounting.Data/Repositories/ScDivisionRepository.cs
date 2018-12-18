using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScDivisionRepository : RepositoryBase<ScDivision>, IScDivisionRepository
    {
        public ScDivisionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IScDivisionRepository : IRepository<ScDivision>
    {
    }
}
