using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScProgramMasterRepository : RepositoryBase<ScProgramMaster>, IScProgramMasterRepository{public ScProgramMasterRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScProgramMasterRepository : IRepository<ScProgramMaster>{}}