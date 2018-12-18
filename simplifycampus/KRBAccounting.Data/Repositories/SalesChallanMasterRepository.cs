using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Data.Infrastructure;

namespace KRBAccounting.Data.Repositories
{
    public class SalesChallanMasterRepository : RepositoryBase<SalesChallanMaster>, ISalesChallanMasterRepository
    {
        public SalesChallanMasterRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface ISalesChallanMasterRepository : IRepository<SalesChallanMaster>
    {
    }
}
