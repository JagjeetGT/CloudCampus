using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class ScSectionRepository : RepositoryBase<ScSection>, IScSectionRepository
    {
        public ScSectionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IScSectionRepository : IRepository<ScSection>
    {

    }
}
