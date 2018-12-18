using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_PurchaseVatRegister
    {
        public int Id { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceMiti { get; set; }
        public string AccountName { get; set; }
        public string VatNo { get; set; }
        public decimal TotalPurchase { get; set; }
        public decimal TaxFreePurchase { get; set; }
        public decimal ZeroRatedPurchase { get; set; }
        public decimal TaxableValue { get; set; }
        public decimal PurchaseTax { get; set; }
    }
}