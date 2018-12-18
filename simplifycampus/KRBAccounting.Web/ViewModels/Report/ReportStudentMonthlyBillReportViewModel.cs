using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportStudentMonthlyBillReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<SP_StudentMonthlyBill> StudentMonthlyList { get; set; }
        public IEnumerable<ScMonthlyBillStudent> BillStudents { get; set; }
        public bool Details { get; set; }
        
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public ScStudentinfo StudentInfo { get; set; }
        public IEnumerable<SP_StudentInfoHeader> SpStudentInfoHeader { get; set; } 

    }
}