using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class DesignRepository : RepositoryBase<Design>, IDesignRepository
    {
        public DesignRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
       
    }

    public interface IDesignRepository : IRepository<Design>
    {
      
    }
}
