using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class AcademicBackgroundRepository : RepositoryBase<AcademicBackground>, IAcademicBackgroundRepository
{
      public AcademicBackgroundRepository(IDatabaseFactory databaseFactory)
          : base(databaseFactory)
{
}
}
  public interface IAcademicBackgroundRepository : IRepository<AcademicBackground>
{
}
}

