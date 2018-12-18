using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScStudentRegistrationRepository : RepositoryBase<ScStudentRegistration>, IScStudentRegistrationRepository
    {
        public ScStudentRegistrationRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IScStudentRegistrationRepository : IRepository<ScStudentRegistration>
    {
    }
}
