using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Data.Repositories
{
   public class ClassIdPercentageRepository : RepositoryBase<ClassIdPercentage>, IClassIdPercentageRepository
   {
        public ClassIdPercentageRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IClassIdPercentageRepository : IRepository<ClassIdPercentage>
    {
    }
}
