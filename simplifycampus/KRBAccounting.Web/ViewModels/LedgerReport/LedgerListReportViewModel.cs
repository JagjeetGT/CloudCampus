using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class LedgerListReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<SP_LedgerListByCategory> LedgerList { get; set; }
        public int LedgerId { get; set; }
        public int ReportType { get; set; }        
        public int GroupBy { get; set; }        
        public bool AllLedger { get; set; }
        public bool InclideOpening { get; set; }
    }
}
