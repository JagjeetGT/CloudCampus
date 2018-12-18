using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportStudentCashCollectionReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<ScBillTransaction> StudentCashCollectionList { get; set; }
        public ScStudentinfo StudentInfo { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public IEnumerable<SP_StudentInfoHeader> SP_StudentInfoHeader { get; set; }
    }
}