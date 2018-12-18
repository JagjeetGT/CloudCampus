using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScAbsentApplicationRepository : RepositoryBase<ScAbsentApplication>, IScAbsentApplicationRepository{public ScAbsentApplicationRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScAbsentApplicationRepository : IRepository<ScAbsentApplication>{}}