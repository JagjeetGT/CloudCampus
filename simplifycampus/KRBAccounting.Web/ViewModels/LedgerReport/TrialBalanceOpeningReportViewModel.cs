using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class TrialBalanceOpeningReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<SP_OpeningTrialBalance> OpeningTrialBalanceList { get; set; }
    }
}