using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScExamAttendanceMasterRepository : RepositoryBase<ScExamAttendanceMaster>, IScExamAttendanceMasterRepository{public ScExamAttendanceMasterRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScExamAttendanceMasterRepository : IRepository<ScExamAttendanceMaster>{}}