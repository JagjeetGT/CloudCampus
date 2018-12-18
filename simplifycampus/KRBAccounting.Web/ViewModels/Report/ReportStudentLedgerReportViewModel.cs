using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportStudentLedgerReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<SP_StudentLedgerSummary> StudentLedgerList { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool Summary { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public IEnumerable<SP_StudentInfoHeader> SpStudentInfoHeader { get; set; }

        public IEnumerable<IEnumerable<ScFeeReceipt>> ScFeeReceipts { get; set; }
    }
}