using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportMarkSheetReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<List<ScExamMarksEntry>> ExamMarksGrouping { get; set; }
        public IEnumerable<ScExamMarksEntry> ExamMarksEntries { get; set; }
        public IEnumerable<ScDivision> Division { get; set; }

        public IEnumerable<ScSection> Section { get; set; }

        


    }
}