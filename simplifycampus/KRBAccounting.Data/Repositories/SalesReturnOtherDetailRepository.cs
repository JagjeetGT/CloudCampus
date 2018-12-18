using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class SalesReturnOtherDetailRepository : RepositoryBase<SalesReturnOtherDetail>, ISalesReturnOtherDetailRepository
    {
        public SalesReturnOtherDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ISalesReturnOtherDetailRepository : IRepository<SalesReturnOtherDetail>
    {
        
    }
}
