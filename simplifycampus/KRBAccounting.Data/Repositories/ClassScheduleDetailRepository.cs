using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ClassScheduleDetailRepository : RepositoryBase<ScClassScheduleDetail>, IClassScheduleDetailRepository{public ClassScheduleDetailRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IClassScheduleDetailRepository : IRepository<ScClassScheduleDetail>{}}