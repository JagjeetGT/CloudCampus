using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class ScStudentinfoRepository : RepositoryBase<ScStudentinfo>, IScStudentinfoRepository
    {
        public ScStudentinfoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public interface IScStudentinfoRepository : IRepository<ScStudentinfo>
    {

    }
}
