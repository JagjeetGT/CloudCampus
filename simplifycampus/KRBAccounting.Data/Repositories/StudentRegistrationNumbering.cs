using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class StudentRegistrationNumberingRepository : RepositoryBase<ScStudentRegistrationNumbering>, IStudentRegistrationNumberingRepository
    {
        public StudentRegistrationNumberingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IStudentRegistrationNumberingRepository : IRepository<ScStudentRegistrationNumbering>
    {
        
    }
}
