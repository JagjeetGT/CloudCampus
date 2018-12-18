using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class CashBankBookReportViewModel : ReportBaseViewModel
    {
        public List<DateTime> DateList { get; set; }
        public int LedgerId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool DisplaySubLedger { get; set; }
        public bool DisplayRemarks{ get; set; }
        public int Datetype { get; set; }
        public DateTime FStartDate { get; set; }
        public int BranchId { get; set; }

    }
}