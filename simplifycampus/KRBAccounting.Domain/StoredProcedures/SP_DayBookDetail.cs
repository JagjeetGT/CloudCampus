using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_DayBookDetail
    {
        public string AccountName { get; set; }
        public string ShortName { get; set; }
        public decimal DrAmt { get; set; }
        public decimal CrAmt { get; set; }
        public string SubLedName { get; set; }
        public string Remarks { get; set; }
        public string Narration { get; set; }
    }
}
