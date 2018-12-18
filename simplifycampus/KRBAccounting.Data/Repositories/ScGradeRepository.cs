using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScGradeRepository : RepositoryBase<ScGrade>, IScGradeRepository
    {
        public ScGradeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IScGradeRepository : IRepository<ScGrade>
    {
    }
}
