using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class ProfitAndLossReportPeriodicViewModel : ReportBaseViewModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string AccountGroupType { get; set; }
        public bool AccountGroup { get; set; }
        public bool Ledger { get; set; }
        public bool Subledger { get; set; }
        public bool AccountSubGroup { get; set; }
        public bool DisplayRemarks { get; set; }
        public int BranchId { get; set; }
    }
}