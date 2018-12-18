using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScTransportMappingRepository : RepositoryBase<ScTransportMapping>, IScTransportMappingRepository{public ScTransportMappingRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScTransportMappingRepository : IRepository<ScTransportMapping>{}}