using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Academy
{
    public class AcademicsExamMarkSetupViewModel : BaseViewModel
    {
        public ScExamMarkSetup ExamMarkSetup { get; set; }
        public IEnumerable<List<ScExamMarkSetup>> ExamMarkSetupGrouping { get; set; }
        public IEnumerable<ScExamMarkSetup> ExamMarkSetups { get; set; }


        public decimal TotalTheoryMark { get; set; }
        public decimal TotalPracticalMark { get; set; }
        public int ExamId { get; set; }
       // [Remote("ConsultancyExamRoutinesExists", "School",AdditionalFields = "ExamId")]
        public int ClassId { get; set; }
    }
}