using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScStudentSubjectMappingRepository : RepositoryBase<ScStudentSubjectMapping>, IScStudentSubjectMappingRepository
    {
        public ScStudentSubjectMappingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        
    }
    public interface IScStudentSubjectMappingRepository : IRepository<ScStudentSubjectMapping>
    {
        
    }
}
