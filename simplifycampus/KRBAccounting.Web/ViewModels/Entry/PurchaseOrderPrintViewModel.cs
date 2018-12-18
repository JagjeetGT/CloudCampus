using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class PurchaseOrderPrintViewModel
    {
        public List<OrderDetail> OrderDetails { get; set; }
        public InvoiceHeader InvoiceHeader { get; set; }
        public double Total { get; set; }
        public double VAT { get; set; }
        public double SubTotal { get; set; }
        public string BilledBy { get; set; }
        public List<OrderBillingTerms> OrderBillingTerms { get; set; }
    }

    public class OrderDetail
    {
        public int SNo { get; set; }
        public string Particular { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
    }

    public class OrderBillingTerms
    {
        public string TermName { get; set; }
        public decimal TermRate { get; set; }
        public decimal TermAmount { get; set; }
    }
}