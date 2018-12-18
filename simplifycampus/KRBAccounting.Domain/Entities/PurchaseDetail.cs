using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class PurchaseDetail
    {
        [Key]
        public int Id { get; set; }

        public int SNo { get; set; }

        //Product Code
        public int PCode { get; set; }

        public decimal? AltQty { get; set; }
        public string AltUnit { get; set; }
        public decimal? Qty { get; set; }
        public string Unit { get; set; }
        public decimal? FreeQty { get; set; }
        public string FreeUnit { get; set; }
        public decimal? AltStockQty { get; set; }
        public decimal StockQty { get; set; }
        public decimal? Rate { get; set; }
        public decimal? BasicAmt { get; set; }
        public decimal TermAmt { get; set; }
        public decimal NetAmt { get; set; }
        public string Remarks { get; set; }
        public int PurchaseInvoiceId { get; set; }
        public int Index { get; set; }
        public int? Godown { get; set; }

        [ForeignKey("PCode")]
        public Product Product { get; set; }

        [NotMapped]
        public int UnitId { get; set; }

        [NotMapped]
        public SelectList Unitlist { get; set; }

        [ForeignKey("PurchaseInvoiceId")]
        public PurchaseInvoice PurchaseInvoice { get; set; }

        [NotMapped]
        public string DetailGuid { get; set; }

        [NotMapped]
        public EntryControlPurchase EntryControl { get; set; }
    }
}
