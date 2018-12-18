using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class TrialBalanceOpening : ReportBaseViewModel
    {
        public bool SubLedger { get; set; }
        public bool AccountGroup { get; set; }
        public bool GeneralLedger { get; set; }
        public string ReportType { get; set; }
        public IEnumerable<SP_OpeningTrialBalance> OpeningLedgers { get; set; }
        public IEnumerable<SP_OpeningTrialBalance> OpeningsGroups { get; set; }
        public IEnumerable<SP_OpeningTrialBalance> OpeningsSubLedgers { get; set; }
        public int BranchId { get; set; }
        public bool CheckBranch { get; set; }
        public SelectList BranchList { get; set; }
    }
}