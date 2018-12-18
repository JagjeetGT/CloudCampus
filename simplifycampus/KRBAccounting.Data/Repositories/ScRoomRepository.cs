using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScRoomRepository : RepositoryBase<ScRoom>, IScRoomRepository{public ScRoomRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScRoomRepository : IRepository<ScRoom>{}}