using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class PurchaseOrderAddViewModel : BaseViewModel
    {
        //public PurchaseOrderImpTransDoc PurchaseOrderImpTransDoc { get; set; }
        
        [DataType(DataType.Date)]
        public string OrderDate { get; set; }
        public string PPDDate { get; set; }
        public string CnDate { get; set; }
        public string ShortName { get; set; }
        public PurchaseOrderMaster PurchaseOrder { get; set; }
        public IEnumerable<UDFEntry> UdfEntries { get; set; }
        public string StockQty { get; set; }
        public string AltStockQty { get; set; }
        public string TotalQty { get; set; }
        public string TotalAltQty { get; set; }
        public string TotalAmt { get; set; }
        public string TotalAmtRs { get; set; }
        public string CurrentBalance { get; set; }
        public string TotalOutstanding { get; set; }
        public SelectList OrderTypeList { get; set; }
        public string DisplayDate { get; set; }
        public bool AllowProductWiseBillTerm { get; set; }
        public SelectList UnitList { get; set; }

        public EntryControlPurchase EntryControl { get; set; }
        
    }
}