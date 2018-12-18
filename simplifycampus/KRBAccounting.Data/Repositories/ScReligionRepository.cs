using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class ScReligionRepository : RepositoryBase<ScReligion>, IScReligionRepository
    {
        public ScReligionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IScReligionRepository : IRepository<ScReligion>
    {
    }
}
