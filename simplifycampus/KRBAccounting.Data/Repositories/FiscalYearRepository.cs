using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class FiscalYearRepository : RepositoryBase<FiscalYear>, IFiscalYearRepository
    {
        public FiscalYearRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IFiscalYearRepository : IRepository<FiscalYear>
    {

    }

    public class AcademicYearRepository : RepositoryBase<AcademicYear>, IAcademicYearRepository
    {
        public AcademicYearRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IAcademicYearRepository : IRepository<AcademicYear>
    {

    }
}
