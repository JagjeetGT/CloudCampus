using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ConsolidateMarkSheetViewModel
    {

        public SelectList Studentinfos { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }

        public List<SchClass> ClassList { get; set; }
        public int ExamId { get; set; }
        public List<SelectListItem> ExamList { get; set; }
   public IEnumerable<ScProgramMaster> ProgramMasters { get; set; }

    }
    public class ReportConsolidateMarkSheetViewModel : ReportBaseViewModel
    {
        public string HTMLDATA { get; set; }
     
    }
}