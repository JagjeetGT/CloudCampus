using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class ShortNameDetailRepository : RepositoryBase<ShortNameDetail>, IShortNameDetailRepository
    {
        public ShortNameDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IShortNameDetailRepository : IRepository<ShortNameDetail>
    {
        
    }
}
