using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScRollNumberingSchemeRepository : RepositoryBase<ScRollNumberingScheme>, IScRollNumberingSchemeRepository
    {
        public ScRollNumberingSchemeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IScRollNumberingSchemeRepository : IRepository<ScRollNumberingScheme>
    {

    }
}
