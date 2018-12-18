using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class ScFeeItemRepository : RepositoryBase<ScFeeItem>, IScFeeItemRepository
    {
        public ScFeeItemRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IScFeeItemRepository : IRepository<ScFeeItem>
    {

    }
}
