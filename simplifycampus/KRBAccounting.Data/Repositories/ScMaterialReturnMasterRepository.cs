using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScMaterialReturnMasterRepository : RepositoryBase<ScMaterialReturnMaster>, IScMaterialReturnMasterRepository{public ScMaterialReturnMasterRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScMaterialReturnMasterRepository : IRepository<ScMaterialReturnMaster>{}}