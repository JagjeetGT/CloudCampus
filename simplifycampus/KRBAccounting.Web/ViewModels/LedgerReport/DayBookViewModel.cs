using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class DayBookViewModel : ReportBaseViewModel
    {
        public IEnumerable<DateTime> DBDateList { get; set; }
        public bool DisplayRemarks { get; set; }
        public bool DisplaySubLedger { get; set; }
        public int Datetype { get; set; }
        public string SourceList { get; set; }
        public string StartDate { get; set; }
        public int BranchId { get; set; }
      
    }
}