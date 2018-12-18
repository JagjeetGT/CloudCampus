using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.LedgerReport
{
    public class CashBankSummaryReportViewModel : ReportBaseViewModel
    {

        public List<SP_CashBankBookSummary> SpCashBankBookSummaries { get; set; }
        public int Datetype { get; set; }
        public bool DateShow { get; set; }
     }
}