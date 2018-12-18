using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScHouseMappingRepository : RepositoryBase<ScHouseMapping>, IScHouseMappingRepository{public ScHouseMappingRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScHouseMappingRepository : IRepository<ScHouseMapping>{}}