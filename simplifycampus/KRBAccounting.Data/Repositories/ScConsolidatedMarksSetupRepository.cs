using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScConsolidatedMarksSetupRepository : RepositoryBase<ScConsolidatedMarksSetup>, IScConsolidatedMarksSetupRepository{public ScConsolidatedMarksSetupRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScConsolidatedMarksSetupRepository : IRepository<ScConsolidatedMarksSetup>{}}