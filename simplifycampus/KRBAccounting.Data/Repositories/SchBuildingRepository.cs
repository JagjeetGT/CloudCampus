using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class SchBuildingRepository : RepositoryBase<SchBuilding>, ISchBuildingRepository
    {
        public SchBuildingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ISchBuildingRepository : IRepository<SchBuilding>
    {
    }
}
