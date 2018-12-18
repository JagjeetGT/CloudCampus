using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class PurchaseProductBatch
    {
        [Key]
        public int Id { get; set; }
        public int DetailId { get; set; }
        public int ProductId { get; set; }
        public string SerialNo { get; set; }
        public decimal Qty { get; set; }
        public int Unit { get; set; }
        public DateTime? MFGDate { get; set; }
        public DateTime? EXPDate { get; set; }
        public decimal? BuyRate { get; set; }
        public decimal? SalesRate { get; set; }
        public int BranchId { get; set; }
        public int? Godown { get; set; }
        public decimal StockQuantity { get; set; }
        public string Source { get; set; }
    }
}
