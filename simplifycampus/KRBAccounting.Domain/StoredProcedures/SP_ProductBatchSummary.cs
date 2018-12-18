using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_ProductBatchSummary
    {
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string SerialNo { get; set; }
        public decimal StockQuantity { get; set; }
        public string Code { get; set; }
        public DateTime? MFGDate { get; set; }
        public DateTime? EXPDate { get; set; }
        public int Days { get; set; }
        public int Expired { get; set; }
      




    }
}
