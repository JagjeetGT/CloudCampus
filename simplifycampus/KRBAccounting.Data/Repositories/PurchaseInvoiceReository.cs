using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Data.Repositories
{
    public class PurchaseInvoiceReository : RepositoryBase<PurchaseInvoice>, IPurchaseInvoiceReository
    {
        public PurchaseInvoiceReository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IPurchaseInvoiceReository : IRepository<PurchaseInvoice>
    {
        
    }
}
