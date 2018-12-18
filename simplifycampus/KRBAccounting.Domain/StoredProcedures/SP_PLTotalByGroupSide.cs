using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_PLTotalByGroupSide
    {
        public string GroupAccountType { get; set; }
        public decimal OpeningTotal { get; set; }
        public decimal PeriodTotal { get; set; }
        public decimal Closing { get; set; }
    }
}
