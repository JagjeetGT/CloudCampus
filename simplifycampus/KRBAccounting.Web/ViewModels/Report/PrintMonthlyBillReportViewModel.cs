using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class PrintMonthlyBillReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<SP_StudentMonthlyBill> StudentMonthlyList { get; set; }
        public IEnumerable<ScMonthlyBillStudent> BillStudents { get; set; }
        public IEnumerable<List<ScMonthlyBillStudent>> BillStudentGrouping { get; set; }
 
    }
}