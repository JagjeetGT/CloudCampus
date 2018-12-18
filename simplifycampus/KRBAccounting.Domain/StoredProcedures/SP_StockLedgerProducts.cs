using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_StockLedgerProducts
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public int UnitId { get; set; }
        public string Code { get; set; }
        public string BatchSerialNo { get; set; }
    }
}
