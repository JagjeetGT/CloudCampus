using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public  class SP_StockLedgerSummary
    {
        public int ProductId { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string UnitShortName { get; set; }
        public decimal OpeningQty { get; set; }
        public decimal ReceivedQty { get; set; }
        public decimal DeliveredQty { get; set; }
        public decimal BalanceQty { get; set; }
        public decimal BalanceValue { get; set; }
        public decimal OpeningValue { get; set; }
        public decimal ReceivedValue { get; set; }
        public decimal DeliveredValue { get; set; }
        
    }
}
