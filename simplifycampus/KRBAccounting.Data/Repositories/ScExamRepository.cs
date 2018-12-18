using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class ScExamRepository : RepositoryBase<ScExam>, IScExamRepository
    {
        public ScExamRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IScExamRepository : IRepository<ScExam>
    {
    }
}
