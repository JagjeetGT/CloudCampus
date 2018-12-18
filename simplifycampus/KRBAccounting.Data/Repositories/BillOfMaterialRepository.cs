using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class BillOfMaterialRepository : RepositoryBase<BillOfMaterial>, IBillOfMaterialRepository
    {
        public BillOfMaterialRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IBillOfMaterialRepository : IRepository<BillOfMaterial>
    {
        
    }
}
