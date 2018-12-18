using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_SalesVatRegister
    {
        public int Id { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceMiti { get; set; }
        public string AccountName { get; set; }
        public string VatNo { get; set; }
        public decimal TotalSale { get; set; }
        public decimal TaxFreeSale { get; set; }
        public decimal ZeroRatedSale { get; set; }
        public decimal TaxableValue { get; set; }
        public decimal SaleTax { get; set; }
    }
}
