using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Service.Models.Sales
{
    public class SalesDetailEntryViewModel
    {
        public int ProductId { get; set; }
        public decimal? AltQty { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Rate { get; set; }
        public decimal? BasicAmt { get; set; }
        public decimal? TermAmt { get; set; }
        public decimal? NetAmt { get; set; }
        public bool AllowProductWiseBillTerm { get; set; }
        public int Index { get; set; }
        public SelectList UnitList { get; set; }
        public int UnitId { get; set; }
        public string AltUnit { get; set; }
        public string ProductName { get; set; }
        public string Godown { get; set; }
        public int? GodownId { get; set; }
        public bool DisableGodown { get; set; }
        public string DetailGuid { get; set; }
        public int? BatchId { get; set; }
        public string BatchSerialNo { get; set; }
        public decimal? StockQty { get; set; }
        public EntryControlSales EntryControl { get; set; }
    }
}