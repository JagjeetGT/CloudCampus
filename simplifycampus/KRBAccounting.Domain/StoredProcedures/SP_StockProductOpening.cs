using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_StockProductOpening
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public decimal OpeningQty { get; set; }
        public decimal OpeningValue { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string BatchSerialNo { get; set; }
        public string Godown { get; set; }
    }
}
