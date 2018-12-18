using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScExamMarkSetupRepository : RepositoryBase<ScExamMarkSetup>, IScExamMarkSetupRepository
    {
        public ScExamMarkSetupRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IScExamMarkSetupRepository : IRepository<ScExamMarkSetup>
    {
    }
}
