using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class SalesInvoiceRepository : RepositoryBase<SalesInvoice>, ISalesInvoiceRepository
    {
        public SalesInvoiceRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ISalesInvoiceRepository : IRepository<SalesInvoice>
    {
        
    }
}
