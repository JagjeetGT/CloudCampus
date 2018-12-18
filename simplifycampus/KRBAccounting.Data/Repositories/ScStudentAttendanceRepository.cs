using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScStudentAttendanceRepository : RepositoryBase<ScStudentAttendance>, IScStudentAttendanceRepository{public ScStudentAttendanceRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScStudentAttendanceRepository : IRepository<ScStudentAttendance>{}}