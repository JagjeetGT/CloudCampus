using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_StockLedgerProductWise
    {
        public DateTime VDate { get; set; }
        public string VMiti { get; set; }
        public int ProductCode { get; set; }
        public string Godown { get; set; }
        public string AccountName { get; set; }
        public string VNo { get; set; }
        public decimal OpeningQty { get; set; }
        public decimal OpeningValue { get; set; }
        public decimal ReceivedQty { get; set; }
        public decimal ReceivedValue { get; set; }
        public decimal DeliveredQty { get; set; }
        public decimal DeliveredValue { get; set; }
        public decimal BalanceQty { get; set; }
        public decimal BalanceValue { get; set; }
        public string Source { get; set; }
        public string BatchSerialNo { get; set; }
    }
}
