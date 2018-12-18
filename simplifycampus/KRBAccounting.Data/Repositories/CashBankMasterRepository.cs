using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class CashBankMasterRepository : RepositoryBase<CashBankMaster>, ICashBankMasterRepository
    {
        public CashBankMasterRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ICashBankMasterRepository : IRepository<CashBankMaster>
    {
        
    }
}
