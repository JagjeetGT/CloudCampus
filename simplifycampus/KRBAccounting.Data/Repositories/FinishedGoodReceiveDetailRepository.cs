using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class FinishedGoodReceiveDetailRepository : RepositoryBase<FinishedGoodReceiveDetail>, IFinishedGoodReceiveDetailRepository
    {
        public FinishedGoodReceiveDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IFinishedGoodReceiveDetailRepository : IRepository<FinishedGoodReceiveDetail>
    {
    }
}
