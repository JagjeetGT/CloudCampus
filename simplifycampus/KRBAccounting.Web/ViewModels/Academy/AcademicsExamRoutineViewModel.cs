using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Academy
{
    public class AcademicsExamRoutineViewModel : BaseViewModel
    {
        public ScExamRoutine ExamRoutine { get; set; }
        public IEnumerable<List<ScExamRoutine>> ExamRoutineGrouping { get; set; }
        public IEnumerable<ScExamRoutine> ExamRoutines { get; set; }

        public int ExamId { get; set; }
       // [Remote("ConsultancyExamRoutinesExists", "School",AdditionalFields = "ExamId")]
        public int ClassId { get; set; }
    }
}