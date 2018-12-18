using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_PurchaseDetailProducts
    {
        public int PurchaseInvoiceId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal BasicAmt { get; set; }
        public string ShortName { get; set; }
        public string Unit { get; set; }
       
    }
}
