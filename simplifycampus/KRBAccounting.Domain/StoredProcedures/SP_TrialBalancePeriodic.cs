using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_TrialBalancePeriodic
    {
        public int AGId { get; set; }
        public string AGDesc { get; set; }
        public int ASGId { get; set; }
        public string ASGDesc { get; set; }
        public string GlCode { get; set; }
        public string ShortName { get; set; }
        public string AccountName { get; set; }
        public int SLCode { get; set; }
        public string SLShortName { get; set; }
        public string SLAccountName { get; set; }
        public decimal OpnDebit { get; set; }
        public decimal OpnCredit { get; set; }
        public decimal PeriodDebit { get; set; }
        public decimal PeriodCredit { get; set; }
        public decimal Balance { get; set; }

    }
}
