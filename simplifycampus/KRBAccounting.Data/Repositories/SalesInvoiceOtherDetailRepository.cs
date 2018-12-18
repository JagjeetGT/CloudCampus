using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class SalesInvoiceOtherDetailRepository : RepositoryBase<SalesInvoiceOtherDetail>, ISalesInvoiceOtherDetailRepository
    {
        public SalesInvoiceOtherDetailRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ISalesInvoiceOtherDetailRepository : IRepository<SalesInvoiceOtherDetail>
    {
        
    }
}
