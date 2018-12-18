using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ConsolidatedLedgerSheetViewModel
    {
        public int ClassId { get; set; }

        public List<SchClass> ClassList { get; set; }
        public int ExamId { get; set; }
        public List<SelectListItem> ExamList { get; set; }
        public string ConsolidatedCode { get; set; }
        public SelectList ConsolidatedList { get; set; }
        public IEnumerable<ScProgramMaster> ProgramMasters { get; set; } 
    }
    public class ReportConsolidatedLedgerSheetViewModel : ReportBaseViewModel
    {
        public string HTMLDATA { get; set; }
        public IEnumerable<StudentRank> StudentRanks { get; set; }
    }
    public class StudentRank
{
        public int StudentId { get; set; }
        public string Rank { get; set; }
}
    public class ConsolidatedLedger
    {
      //  rd.StudentId,s.StuDesc,IsNull(sec.Description,'') Description ,rd.RollNo ,m.TotalFullMarks,
          //  m.TotalObtainedMarks,m.Percentage,m.Result,d.Description

        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Section { get; set; }
        public string RollNo { get; set; }
        public decimal TotalFullMarks { get; set; }
        public decimal TotalObtainedMarks { get; set; }
        public decimal Percentage { get; set; }
        public string Result { get; set; }
        public string Division { get; set; }
    }
}