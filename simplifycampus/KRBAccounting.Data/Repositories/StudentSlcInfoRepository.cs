using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class StudentSlcInfoRepository : RepositoryBase<StudentSlcInfo>, IStudentSlcInfoRepository
    {
        public StudentSlcInfoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IStudentSlcInfoRepository : IRepository<StudentSlcInfo>
    {
        
    }
}
