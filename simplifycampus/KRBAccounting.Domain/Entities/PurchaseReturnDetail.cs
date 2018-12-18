using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class PurchaseReturnDetail
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
        public string Remarks { get; set; }
        public string AddDesc { get; set; }
        public decimal? FreeQty { get; set; }
        public decimal? StockFreeQty { get; set; }
        public string FreeUnit { get; set; }
        public decimal? ConvRatio { get; set; }
        public decimal? TaxPriceRate { get; set; }
        public int? TaxGroupId { get; set; }
        public bool TaxInclusiveItem { get; set; }
        public bool BillRateInclusive { get; set; }
        public int PurchaseReturnId { get; set; }
        public int Index { get; set; }
        public int? BatchId { get; set; }
        [ForeignKey("ProductCode")]
        public Product Product { get; set; }

        [ForeignKey("PurchaseReturnId")]
        public PurchaseReturnMaster PurchaseReturnMaster { get; set; }

        [NotMapped]
        public SelectList UnitList { get; set; }
        [NotMapped]
        public int UnitId { get; set; }

        [NotMapped]
        public string BatchSerialNo { get; set; }

        [NotMapped]
        public EntryControlPurchase EntryControl { get; set; }

        [NotMapped]
        public string DetailGuid { get; set; }
    }
}
