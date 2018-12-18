using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScPrePaidSchemeRepository : RepositoryBase<ScPrePaidScheme>, IScPrePaidSchemeRepository
    {
        public ScPrePaidSchemeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IScPrePaidSchemeRepository : IRepository<ScPrePaidScheme>
    {
    }
}
