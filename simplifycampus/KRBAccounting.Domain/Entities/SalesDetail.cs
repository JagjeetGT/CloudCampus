using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class SalesDetail
    {
        [Key]
        public int Id { get; set; }

        public int SNo { get; set; }
        public int? Godown { get; set; }
        //Product Code
        public int ProductCode { get; set; }

        public decimal? AltQty { get; set; }
        public string AltUnit { get; set; }
        public decimal? Qty { get; set; }
        public string Unit { get; set; }
        public decimal? AltStockQty { get; set; }
        public decimal StockQty { get; set; }
        public decimal? Rate { get; set; }
        public decimal? BasicAmt { get; set; }
        public decimal TermAmt { get; set; }
        public decimal NetAmt { get; set; }
        public string Remarks { get; set; }
        public string AddDesc { get; set; }
        public string ChallanNo { get; set; }
        public string OrderNo { get; set; }
        public int OrderSNo { get; set; }
        public decimal? FreeQty { get; set; }
        public decimal? StockFreeQty { get; set; }
        public string FreeUnit { get; set; }
        public decimal? ConvRatio { get; set; }
        public bool CItem { get; set; }
        public string DisorderNo { get; set; }
        public int DisorderSNo { get; set; }
        public decimal? ExtraFreeQty { get; set; }
        public decimal? ExtraStockFreeQty { get; set; }
        public string ExtraFreeUnit { get; set; }
        public decimal? TaxPriceRate { get; set; }
        public string TaxGroupId { get; set; }
        public string CalculateTaxItem { get; set; }
        public bool TaxInclusiveItem { get; set; }
        public bool BillRateInclusive { get; set; }
        public string BillSys { get; set; }
        public int SalesInvoiceId { get; set; }
        public int Index { get; set; }
        public int? BatchId { get; set; }
        [ForeignKey("SalesInvoiceId")]
        public SalesInvoice SalesInvoice { get; set; }

        [ForeignKey("ProductCode")]
        public virtual Product Product { get; set; }

        [NotMapped]
        public SelectList Unitlist { get; set; }

        [NotMapped]
        public int UnitId { get; set; }

        [NotMapped]
        public string BatchSerialNo{ get; set; }

        [NotMapped]
        public EntryControlSales EntryControl { get; set; }

        [NotMapped]
        public string DetailGuid { get; set; }
    }
}
