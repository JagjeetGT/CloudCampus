using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class AccountWatchListRepository : RepositoryBase<AccountWatchList>, IAccountWatchListRepository
    {
        public AccountWatchListRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
     
    }

    public interface IAccountWatchListRepository : IRepository<AccountWatchList>
    {
    }
}
