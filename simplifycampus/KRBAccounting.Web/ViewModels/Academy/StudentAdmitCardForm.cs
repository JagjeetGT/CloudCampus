using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRBAccounting.Web.ViewModels.Academy
{
    public class StudentAdmitCardForm
    {
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> SectionList { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public List<SelectListItem> ExamList { get; set; }
        public int ExamId { get; set; }
    }
}