using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScExamRoutineRepository : RepositoryBase<ScExamRoutine>, IScExamRoutineRepository
    {
        public ScExamRoutineRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IScExamRoutineRepository : IRepository<ScExamRoutine>
    {
    }
}
