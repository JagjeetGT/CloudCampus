using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class BillOfMaterialDetailRepository : RepositoryBase<BillOfMaterialDetail>, IBillOfMaterialDetailRepository
    {
        public BillOfMaterialDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IBillOfMaterialDetailRepository : IRepository<BillOfMaterialDetail>
    {

    }
}
