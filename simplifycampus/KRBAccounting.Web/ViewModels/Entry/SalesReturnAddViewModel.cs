using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class SalesReturnAddViewModel : BaseViewModel
    {
        public SalesReturnMaster SalesReturnMaster { get; set; }
        public IEnumerable<UDFEntry> UdfEntries { get; set; } 
        public SalesReturnOtherDetail SalesReturnOtherDetail { get; set; }
        public string InvoiceDate { get; set; }
        [Required(ErrorMessage = " ")]
        public string RefDate { get; set; }
        public string DueDate { get; set; }
        public string CnDate { get; set; }
        public string PODate { get; set; }
        public string ContractDate { get; set; }
        public string ExportInvoiceDate { get; set; }
        public string ShortName { get; set; }
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
        public string DisplayDate { get; set; }
        public DateTime Date { get; set; }
        public SelectList CnTypeList { get; set; }
        public bool AllowProductWiseBillTerm { get; set; }

        public SelectList Unitlist { get; set; }
        public EntryControlSales EntryControl { get; set; }
    }
}