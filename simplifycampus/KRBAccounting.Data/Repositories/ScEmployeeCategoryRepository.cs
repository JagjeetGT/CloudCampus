using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScEmployeeCategoryRepository : RepositoryBase<ScEmployeeCategory>, IScEmployeeCategoryRepository
    {
        public ScEmployeeCategoryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IScEmployeeCategoryRepository : IRepository<ScEmployeeCategory>
    {
    }
}
