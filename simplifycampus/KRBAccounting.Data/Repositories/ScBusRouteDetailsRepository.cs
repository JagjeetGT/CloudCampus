using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class ScBusRouteDetailsRepository : RepositoryBase<ScBusRouteDetails>, IScBusRouteDetailsRepository
    {
        public ScBusRouteDetailsRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IScBusRouteDetailsRepository : IRepository<ScBusRouteDetails> 
    {
    }
} 
