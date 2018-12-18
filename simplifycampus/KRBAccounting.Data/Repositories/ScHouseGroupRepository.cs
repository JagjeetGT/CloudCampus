using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScHouseGroupRepository : RepositoryBase<ScHouseGroup>, IScHouseGroupRepository{public ScHouseGroupRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScHouseGroupRepository : IRepository<ScHouseGroup>{}}