using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScLibraryMemberRegistrationRepository : RepositoryBase<ScLibraryMemberRegistration>, IScLibraryMemberRegistrationRepository
    {
        public ScLibraryMemberRegistrationRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
    public interface IScLibraryMemberRegistrationRepository : IRepository<ScLibraryMemberRegistration>
    {
       
    }
}
