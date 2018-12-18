
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class ScStudentCategoryRepository : RepositoryBase<ScStudentCategory>, IScStudentCategoryRepository
    {
        public ScStudentCategoryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IScStudentCategoryRepository : IRepository<ScStudentCategory>
    {

    }
}
