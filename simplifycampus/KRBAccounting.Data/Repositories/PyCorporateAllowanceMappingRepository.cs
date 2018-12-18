using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class PyCorporateAllowanceMappingRepository : RepositoryBase<PyCorporateAllowanceMapping>, IPyCorporateAllowanceMappingRepository
    {
        public PyCorporateAllowanceMappingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IPyCorporateAllowanceMappingRepository : IRepository<PyCorporateAllowanceMapping>
    {
    }
}
