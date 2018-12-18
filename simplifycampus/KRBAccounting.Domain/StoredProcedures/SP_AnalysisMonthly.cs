using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_AnalysisMonthly
    {
        public int Id { get; set; }
        public DateTime AsOnDate { get; set; }
        public decimal Purchase { get; set; }
        public decimal Sales { get; set; }
        public decimal SalesReturn { get; set; }
        public decimal PurchaseReturn { get; set; }
    }
}
