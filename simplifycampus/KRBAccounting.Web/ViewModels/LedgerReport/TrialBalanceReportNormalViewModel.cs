using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class TrialBalanceReportNormalViewModel : ReportBaseViewModel
    {
        public IEnumerable<SP_TrialBalanceLedgerWiseAsOnDate> ReportList { get; set; }
        public decimal Total { get; set; }
        public bool SubLedger { get; set; }
    }
}