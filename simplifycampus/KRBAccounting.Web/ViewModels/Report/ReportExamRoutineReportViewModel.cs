using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportExamRoutineReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<ScExamMarkSetup> ExamMarkSetUps { get; set; }
        public IEnumerable<ScExamRoutine> ExamRoutines { get; set; }
        public IEnumerable<List<ScExamRoutine>> ExamRoutineGrouping { get; set; }
        public IEnumerable<List<ScExamMarkSetup>> ExamMarkSetupGrouping { get; set; }
        public string ExamRoutineFooter { get; set; }

        public SelectList ExamList { get; set; }
        public int ExamId { get; set; }
        
    }
}