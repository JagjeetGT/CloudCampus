using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class ExtraTermRepository : RepositoryBase<ExtraTerm>, IExtraTermRepository
    {
        public ExtraTermRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IExtraTermRepository : IRepository<ExtraTerm>
    {
    }




    public class ExtraTermDetailRepository : RepositoryBase<ExtraTermDetail>, IExtraTermDetailRepository
    {
        public ExtraTermDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IExtraTermDetailRepository : IRepository<ExtraTermDetail>
    {
    }
}
