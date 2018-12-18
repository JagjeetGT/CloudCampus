using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Service.Models.Purchase
{
    public class PurchaseDetailEntryViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? AltQty { get; set; }
        public decimal? Qty { get; set; }
        public string AltUnit { get; set; }
        public int Unit { get; set; }
        public decimal? Rate { get; set; }
        public decimal? BasicAmt { get; set; }
        public decimal? TermAmt { get; set; }
        public decimal? NetAmt { get; set; }
        public bool AllowProductWiseBillTerm { get; set; }
        public int Index { get; set; }
        public decimal? popoverUnit { get; set; }
        public decimal? popooverQuantity { get; set; }
        public string Godown { get; set; }
        public int? GodownId { get; set; }
        public bool DisableGodown { get; set; }
        public string DetailGuid { get; set; }
        public SelectList UnitList { get; set; }
        public ActionResult HiddenRate { get; set; }
        public EntryControlPurchase EntryControl { get; set; }
    }
}
