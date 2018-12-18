using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class TrialBalanceReportGroupWiseViewModel : ReportBaseViewModel
    {
        public IEnumerable<SP_TrialBalanceAcGroupWiseAsOnDate> ReportList { get; set; }
        public decimal Total { get; set; }
        public bool Ledger { get; set; }
        public bool SubLedger { get; set; }
        public bool AccountSubGroup { get; set; }
    }
}