using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportMarkSheetFormViewModel : BaseViewModel
    {
        public List<IGrouping<int, ScStudentRegistrationDetail>> StudentinfoList { get; set; }
        public bool SelectAll { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> ExamList { get; set; }
        public IEnumerable<ScProgramMaster> ProgramMasters { get; set; } 
        public int ClassId { get; set; }
        public int ExamId { get; set; }
 
    }
}