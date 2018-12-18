using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_PurchaseDetail
    {
        public int Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceNo { get; set; }
        public int GlCode { get; set; }
        public string AccountName { get; set; }
        public decimal NetAmt { get; set; }
        public string Remarks { get; set; }
        public string InvoiceMiti { get; set; }


}
}
