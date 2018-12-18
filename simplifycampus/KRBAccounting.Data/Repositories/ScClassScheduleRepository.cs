using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScClassScheduleRepository : RepositoryBase<ScClassSchedule>, IScClassScheduleRepository{public ScClassScheduleRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScClassScheduleRepository : IRepository<ScClassSchedule>{}}