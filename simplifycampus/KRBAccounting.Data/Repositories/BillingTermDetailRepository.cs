using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class BillingTermDetailRepository: RepositoryBase<BillingTermDetail>, IBillingTermDetailRepository
    {
        public BillingTermDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IBillingTermDetailRepository : IRepository<BillingTermDetail>
    {
    }
}
