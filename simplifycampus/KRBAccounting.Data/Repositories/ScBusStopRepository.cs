using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScBusStopRepository : RepositoryBase<ScBusStop>, IScBusStopRepository{public ScBusStopRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScBusStopRepository : IRepository<ScBusStop>{}}