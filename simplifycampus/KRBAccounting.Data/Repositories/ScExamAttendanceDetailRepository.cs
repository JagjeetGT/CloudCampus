using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScExamAttendanceDetailRepository : RepositoryBase<ScExamAttendanceDetail>, IScExamAttendanceDetailRepository{public ScExamAttendanceDetailRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScExamAttendanceDetailRepository : IRepository<ScExamAttendanceDetail>{}}