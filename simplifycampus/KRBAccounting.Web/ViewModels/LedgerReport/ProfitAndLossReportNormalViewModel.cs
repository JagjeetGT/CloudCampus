using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class ProfitAndLossReportNormalViewModel : ReportBaseViewModel
    {
        public string AsOnDate { get; set; }
        public string AccountGroupType { get; set; }
        public bool AccountGroup { get; set; }
        public bool Ledger { get; set; }
        public bool Subledger { get; set; }
        public bool AccountSubGroup { get; set; }
        public int BranchId { get; set; }
    }
}