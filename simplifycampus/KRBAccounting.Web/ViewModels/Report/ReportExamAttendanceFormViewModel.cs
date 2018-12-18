using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportExamAttendanceFormViewModel
    {
        public List<SelectListItem> SectionList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public SelectList ExamList { get; set; }
        public int ExamId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
       
    }
}