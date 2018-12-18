using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Service.Models.BillingTerm;

namespace KRBAccounting.Service.Models.Purchase
{
    public class PurchaseInvoiceAddViewModel : BaseModel
    {
        public PurchaseImpTransDoc PurchaseImpTransDoc { get; set; }
        public String InvoiceDate { get; set; }
        public string PartyDate { get; set; }
        public string DueDate { get; set; }
        public string PPDDate { get; set; }
        public string CnDate { get; set; }
        public string ShortName { get; set; }
        public PurchaseInvoice PurchaseInvoice { get; set; }
        public IEnumerable<UDFEntry> UdfEntries { get; set; }
        public string StockQty { get; set; }
        public string AltStockQty { get; set; }
        public string TotalQty { get; set; }
        public string TotalAltQty { get; set; }
        public string TotalAmt { get; set; }
        public string TotalAmtRs { get; set; }

        public string CreditLimit { get; set; }
        public string CurrentBalance { get; set; }
        public string OutstandingChallan { get; set; }
        public string TotalOutstanding { get; set; }

        public SelectList InvoiceTypeList { get; set; }

        public string DisplayDate { get; set; }

        public bool AllowProductWiseBillTerm { get; set; }
        public SelectList UnitList { get; set; }

        public string ChallanNo { get; set; }

        public string Godown { get; set; }
        public int GodownId { get; set; }
        public int? PurchaseOrderId { get; set; }
        public string OrderNo { get; set; }
        public IEnumerable<PurchaseProductBatchViewModel> PurchaseProductBatchViewModels { get; set; }
        public int ExpiredProduct { get; set; }
        public EntryControlPurchase EntryControl { get; set; }

        public IEnumerable<PurchaseDetailEntryViewModel> purchaseDetailEntry { get; set; }

        public IEnumerable<BillingTermDetailViewModel> billTermList { get; set; }

        public IEnumerable<PurchaseProductBatchViewModel> ProductBatchList { get; set; }

        public List<BillingTermSelectViewModel> ProductWiseBillTerms { get; set; }
        public List<BillingTermSelectViewModel> BillWiseBillTerms { get; set; }
    }
}
