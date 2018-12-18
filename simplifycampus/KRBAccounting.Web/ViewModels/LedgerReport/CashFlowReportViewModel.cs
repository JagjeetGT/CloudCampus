using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class CashFlowReportViewModel : ReportBaseViewModel
    {
        public List<DateTime> DateList { get; set; }
        public int LedgerId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int branchId { get; set; }
    }
}
