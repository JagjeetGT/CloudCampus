using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScFineSchemeDetailsRepository : RepositoryBase<ScFineSchemeDetails>, IScFineSchemeDetailsRepository
    {
        public ScFineSchemeDetailsRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IScFineSchemeDetailsRepository : IRepository<ScFineSchemeDetails>
    {
    }
}
