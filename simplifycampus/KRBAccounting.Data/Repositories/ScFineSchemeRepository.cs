using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScFineSchemeRepository : RepositoryBase<ScFineScheme>, IScFineSchemeRepository
    {
        public ScFineSchemeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IScFineSchemeRepository : IRepository<ScFineScheme>
    {
    }
}
