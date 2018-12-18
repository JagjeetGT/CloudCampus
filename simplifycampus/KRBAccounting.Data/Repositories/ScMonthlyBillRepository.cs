using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class ScMonthlyBillRepository : RepositoryBase<ScMonthlyBill>, IScMonthlyBillRepository
    {
        public ScMonthlyBillRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IScMonthlyBillRepository : IRepository<ScMonthlyBill>
    {
    }
}
