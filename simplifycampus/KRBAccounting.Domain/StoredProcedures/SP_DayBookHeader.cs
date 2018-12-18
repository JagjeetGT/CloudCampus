using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_DayBookHeader
    {
        public DateTime VDate { get; set; }
        public string VMiti { get; set; }
        public string VNo { get; set; }
        public string Source { get; set; }
        public decimal DrAmt { get; set; }
        public decimal CrAmt { get; set; }
        public string Remarks { get; set; }
    }
}
