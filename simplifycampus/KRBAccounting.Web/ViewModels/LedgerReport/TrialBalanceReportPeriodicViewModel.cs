using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class TrialBalanceReportPeriodicViewModel : ReportBaseViewModel
    {
        public IEnumerable<SP_TrialBalancePeriodic> ReportList { get; set; }
        public decimal OpeningTotal { get; set; }
        public decimal PeriodTotal { get; set; }
        public decimal ClosingTotal { get; set; }
        public bool AccountGroup { get; set; }
        public bool AccountSubGroup { get; set; }
        public bool Ledger { get; set; }
        public bool SubLedger { get; set; }
    }
}