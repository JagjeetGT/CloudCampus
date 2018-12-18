using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class ScClassRoomMappingRepository : RepositoryBase<ScClassRoomMapping>, IScClassRoomMappingRepository{public ScClassRoomMappingRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface IScClassRoomMappingRepository : IRepository<ScClassRoomMapping>{}}