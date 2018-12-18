using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScShiftRepository : RepositoryBase<ScShift>, IScShiftRepository{public ScShiftRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScShiftRepository : IRepository<ScShift>{}}