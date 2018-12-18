using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class StockTransferDetail
    {
        [Key]
        public int Id { get; set; }
        public int StockTransferMasterId { get; set; }

        [ForeignKey("StockTransferMasterId")]
        public virtual StockTransferMaster StockTransferMaster { get; set; }
        public int SNo { get; set; }
        public int Godown { get; set; }
        public decimal? AltQty { get; set; }
        public string AltUnit { get; set; }
        public decimal Qty { get; set; }
        public int UnitId { get; set; }
        public decimal? AltStockQty { get; set; }
        public decimal StockQty { get; set; }
        public decimal Rate { get; set; }
        public decimal NetAmt { get; set; }
        public int ProductCode { get; set; }

        
      
    }
}
