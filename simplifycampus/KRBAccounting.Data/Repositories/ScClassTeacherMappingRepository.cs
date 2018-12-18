using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScClassTeacherMappingRepository : RepositoryBase<ScClassTeacherMapping>, IScClassTeacherMappingRepository
    {
        public ScClassTeacherMappingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
    }

    public interface IScClassTeacherMappingRepository : IRepository<ScClassTeacherMapping>
    {
        
    }
}
