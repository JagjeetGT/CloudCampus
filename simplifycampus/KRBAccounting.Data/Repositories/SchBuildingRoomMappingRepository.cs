using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{public class SchBuildingRoomMappingRepository : RepositoryBase<SchBuildingRoomMapping>, ISchBuildingRoomMappingRepository{public SchBuildingRoomMappingRepository(IDatabaseFactory databaseFactory): base(databaseFactory){}}public interface ISchBuildingRoomMappingRepository : IRepository<SchBuildingRoomMapping>{}}