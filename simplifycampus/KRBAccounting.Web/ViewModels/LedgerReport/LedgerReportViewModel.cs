using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class LedgerReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<SP_Ledgers> LedgerList { get; set; }
        public string StartDate { get; set; }
        public string DisplayreportType { get; set; }
        public string EndDate { get; set; }
        public bool DisplaySubledger { get; set; }
        public string SubLedgers { get; set; }
        public bool DisplayRemarks { get; set; }
        public int GroupBy { get; set; }
        public bool DocAgent { get; set; }
        public bool Summary { get; set; }
        public bool DisplayProductDetails { get; set; }
        public bool DisplayTermDetails { get; set; }
        public bool DisplayZeroBalance { get; set; }
        public int Datetype { get; set; }
        public int BranchId { get; set; }

    }
}