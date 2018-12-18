using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScClassSubjectMappingRepository : RepositoryBase<ScClassSubjectMapping>, IScClassSubjectMappingRepository
    {
        public ScClassSubjectMappingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        
    }
    public interface IScClassSubjectMappingRepository : IRepository<ScClassSubjectMapping>
    {
        
    }
}
