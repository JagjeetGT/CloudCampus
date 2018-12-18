using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class SalesReturnDetailEntryViewModel
    {
        public int ProductId { get; set; }
        public decimal? AltQty { get; set; }
        public decimal? Qty { get; set; }
        public decimal? FreeQty { get; set; }
        public string FreeUnit { get; set; }
        public decimal? Rate { get; set; }
        public decimal? BasicAmt { get; set; }
        public decimal? TermAmt { get; set; }
        public decimal? NetAmt { get; set; }
        public bool AllowProductWiseBillTerm { get; set; }
        public int Index { get; set; }
        public SelectList UnitList { get; set; }
        public string Godown { get; set; }
        public int? GodownId { get; set; }
        public int UnitId { get; set; }
        public string AltUnit { get; set; }
        public Product Product { get; set; }
        public string DetailGuid { get; set; }
        public EntryControlSales EntryControl { get; set; }
    }
}