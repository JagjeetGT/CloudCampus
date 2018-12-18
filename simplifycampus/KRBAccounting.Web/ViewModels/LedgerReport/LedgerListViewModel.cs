using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class LedgerListViewModel : BaseViewModel
    {
        public int LedgerId { get; set; }
        public int ReportType { get; set; }
        public SelectList ReportTypeList { get; set; }
        public int GroupBy { get; set; }
        public SelectList GroupByList { get; set; }
        public bool AllLedger { get; set; }
        public bool InclideOpening { get; set; }
    }
}
