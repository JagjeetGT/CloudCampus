using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class UniversityMarkLedgerViewModel : ReportBaseViewModel
    {
        public ScStudentinfo Studentinfo { get; set; }
        public BoardExamMaster BoardExamMaster { get; set; }
        public IEnumerable<BoardExamDetail> BoardExamDetails { get; set; }

        public int ProgramId { get; set; }
        public int StudentId { get; set; }

        public string Durationtype { get; set; }

        public List<SelectListItem> StudentList { get; set; }
        public List<SelectListItem> ProgramList { get; set; }

        public IEnumerable<Sp_GetSubjectByClassIdandStudentIdResult> SpGetSubjectByClassIdandStudentIdResult { get; set; }

        public IEnumerable<AcademicBackground> AcademicBackgrounds { get; set; }
        public IEnumerable<SP_StudentInfoHeader> SpStudentInfoHeader { get; set; }
    }
}