using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class ScNationalityRepository : RepositoryBase<ScNationality>, IScNationalityRepository
    {
        public ScNationalityRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IScNationalityRepository : IRepository<ScNationality>
    {
    }
}
