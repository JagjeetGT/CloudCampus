using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportExamRoutineFormViewModel
    {
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> ExamList { get; set; }
        public List<SelectListItem> SubjectList { get; set; }
        public int ClassId { get; set; }
        public int ExamId { get; set; }
        public int SubjectId { get; set; }

    }

    
}