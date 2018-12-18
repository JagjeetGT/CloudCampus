using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_TrialBalanceLedgerWiseAsOnDate
    {
        public int GlCode { get; set; }
        public string ShortName { get; set; }
        public string AccountName { get; set; }
        public int SlCode { get; set; }
        public string SLShortName { get; set; }
        public string SLAccountName { get; set; }
        public decimal DebitCredit { get; set; }
    }
}
