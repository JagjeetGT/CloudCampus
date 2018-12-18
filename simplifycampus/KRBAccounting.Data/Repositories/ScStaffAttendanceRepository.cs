using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScStaffAttendanceRepository : RepositoryBase<ScStaffAttendance>, IScStaffAttendanceRepository{public ScStaffAttendanceRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScStaffAttendanceRepository : IRepository<ScStaffAttendance>{}}