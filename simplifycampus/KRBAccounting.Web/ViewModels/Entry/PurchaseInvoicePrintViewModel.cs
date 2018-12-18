using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class PurchaseInvoicePrintViewModel : ReportBaseViewModel
    {
        public List<InvoiceDetail> InvoiceDetails { get; set; }
        public InvoiceHeader InvoiceHeader { get; set; }
        public double Total { get; set; }
        public double VAT { get; set; }
        public double SubTotal { get; set; }
        public string BilledBy { get; set; }
        public List<InvoiceBillingTerms> InvoiceBillingTerms { get; set; }
        public PurchaseInvoice PurchaseInvoice { get; set; }
        public PurchaseReturnMaster PurchaseReturnMaster { get; set; }
        public SalesInvoice SalesInvoice { get; set; }
        public SalesReturnMaster SalesReturnMaster { get; set; }
        public string AgentName { get; set; }
    }

    public class InvoiceDetail
    {
        public int SNo { get; set; }
        public int Id { get; set; }
        public string Particular { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public string Godown { get; set; }
        public int? GodownId { get; set; }
    }

    public class InvoiceBillingTerms
    {
        public string Type { get; set; }
        public string TermName { get; set; }
        public decimal TermRate { get; set; }
        public decimal TermAmount { get; set; }
        public int DisplayOrder { get; set; }
        public int DetailId { get; set; }

    }


}