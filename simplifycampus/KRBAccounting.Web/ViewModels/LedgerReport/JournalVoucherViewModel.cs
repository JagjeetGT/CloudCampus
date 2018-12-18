using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class JournalVoucherViewModel : ReportBaseViewModel
    {
        public IEnumerable<DateTime> JVDateList { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }  
        public bool DisplayRemarks { get; set; }
        public bool DisplayDetails { get; set; }
        public bool DisplaySubLedger { get; set; }
        public int Datetype { get; set; }
    }
}