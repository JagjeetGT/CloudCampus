using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class CashBankDetailRepository : RepositoryBase<CashBankDetail>, ICashBankDetailRepository
    {
        public CashBankDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ICashBankDetailRepository : IRepository<CashBankDetail>
    {
        
    }
}
