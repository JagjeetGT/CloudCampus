using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class PurchaseOrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? SNo { get; set; }
        public int? ProductCode { get; set; }
        public decimal? AltQty { get; set; }
        public string AltUnit { get; set; }
        public decimal? Qty { get; set; }
        public string Unit { get; set; }
        public decimal? AltStockQty { get; set; }
        public decimal? StockQty { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Productamt { get; set; }
        public decimal? TermAmt { get; set; }
        public decimal? NetAmt { get; set; }
        public decimal? BasicAmt { get; set; }
        public string Remarks { get; set; }
        public decimal? IssueQty { get; set; }
        public decimal? POConvRatio { get; set; }

        [NotMapped]
        public int UnitId { get; set; }

        [NotMapped]
        public SelectList Unitlist { get; set; }

        [ForeignKey("OrderId")]
        public virtual PurchaseOrderMaster PurchaseOrderMaster { get; set; }

        [NotMapped]
        public string DetailGuid { get; set; }

        [NotMapped]
        public EntryControlPurchase EntryControl { get; set; }
    }
}
