using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScBookIssueReturnRepository : RepositoryBase<ScBookIssueReturn>, IScBookIssueReturnRepository
    {
        public ScBookIssueReturnRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IScBookIssueReturnRepository : IRepository<ScBookIssueReturn>
    {
    }
}
