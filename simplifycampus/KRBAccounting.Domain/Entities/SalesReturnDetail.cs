using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class SalesReturnDetail
    {
        [Key]
        public int Id { get; set; }
        public int SNo { get; set; }
        public int ProductCode { get; set; }
        public int? Godown { get; set; }
        public decimal? AltQty { get; set; }
        public string AltUnit { get; set; }
        public decimal? Qty { get; set; }
        public string Unit { get; set; }
        public decimal? AltStockQty { get; set; }
        public decimal StockQty { get; set; }
        public decimal? Rate { get; set; }
        public decimal? BasicAmt { get; set; }
        public decimal? TermAmt { get; set; }
        public decimal NetAmt { get; set; }
        public decimal? StockRate { get; set; }
        public decimal? StockAmt { get; set; }
        public string Remarks { get; set; }
        public string AddDesc { get; set; }
        //public decimal? FreeQty { get; set; }
        public decimal StockFreeQty { get; set; }
        public string FreeUnit { get; set; }
        public decimal ConvRatio { get; set; }
        public decimal ExtraFreeQty { get; set; }
        public decimal ExtraStockFreeQty { get; set; }
        public string ExtraFreeUnit { get; set; }
        public decimal TaxPriceRate { get; set; }
        public string TaxGroupId { get; set; }
        public string CalculateTaxItem { get; set; }
        public bool TaxInclusiveItem { get; set; }
        public bool BillRateInclusive { get; set; }
        public int SalesReturnId { get; set; }
        public int Index { get; set; }
        [ForeignKey("SalesReturnId")]
        public SalesReturnMaster SalesReturnMaster { get; set; }

        [ForeignKey("ProductCode")]
        public Product Product { get; set; }

        [NotMapped]
        public int UnitId { get; set; }

        [NotMapped]
        public SelectList UnitList { get; set; }

        [NotMapped]
        public string DetailGuid { get; set; }

        [NotMapped]
        public EntryControlSales EntryControl { get; set; }
    }
}
