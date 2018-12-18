using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class StockTransaction
    {
        [Key]
        public int Id { get; set; }
        public string VNo { get; set; }
        public DateTime VDate { get; set; }
        public string VMiti { get; set; }
        public int GlCode { get; set; }
        public string AgentCode { get; set; }
        public string CurrCode { get; set; }
        public decimal? CurrRate { get; set; }
        public int SNo { get; set; }
        public int ProductCode { get; set; }
        public string Godown { get; set; }
        public decimal? AltQty { get; set; }
        public string AltUnit { get; set; }
        public decimal? Quantity { get; set; }
        public string Unit { get; set; }
        public decimal? AltStockQty { get; set; }
        public decimal StockQty { get; set; }
        public decimal? FreeQty { get; set; }
        public decimal? StockFreeQty { get; set; }
        public string FreeUnit { get; set; }
        public decimal? STConvRatio { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal? TermAmt { get; set; }
        public decimal NetAmt { get; set; }
        public decimal StockVal { get; set; }
        public string TransactionType { get; set; }
        public string Source { get; set; }
        public int ReferenceId { get;set; }
        public int FYId  { get; set; }
        public string BatchSerialNo { get; set; }
        //Godown Code
        public string GdnCode { get; set; }

        public decimal? DocVal { get; set; }
        public decimal? TmpStockVal { get; set; }
        public decimal? BillTerm { get; set; }
        public decimal? ExtraFreeQty { get; set; }
        public decimal? ExtraStockFreeQty { get; set; }
        public string ExtraFreeUnit { get; set; }
        public int BranchId { get; set; }

        [ForeignKey("BranchId")]
        public CompanyInfo Branch { get; set; }
    }
}
