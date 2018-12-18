using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScTeacherScheduleRepository : RepositoryBase<ScTeacherSchedule>, IScTeacherScheduleRepository
    {
        public ScTeacherScheduleRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IScTeacherScheduleRepository : IRepository<ScTeacherSchedule>
    {
    }
}
