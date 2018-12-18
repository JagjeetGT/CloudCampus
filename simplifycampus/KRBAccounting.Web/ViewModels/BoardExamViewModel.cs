using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels
{
    public class BoardExamViewModel
    {
        public BoardExamMaster BoardExamMaster { get; set; }
        public IEnumerable<BoardExamDetail> BoardExamDetail { get; set; }
        public IEnumerable<BoardExamDetailAddViewModel> BoardExamDetailAddViewModel { get; set; }
        public List<SelectListItem> StudentList { get; set; }




        public List<SelectListItem> ProgramList { get; set; }
        public string CharacterDisplayDate { get; set; }
        public string TranscriptDisplayDate { get; set; }
        public string Durationtype { get; set; }
        public SystemControl SystemControl { get; set; }

        public List<SelectListItem> DivisionList { get; set; }

        public IEnumerable<Sp_GetSubjectByClassIdandStudentIdResult> SpGetSubjectByClassIdandStudentIdResult { get; set; }

        public string ProgramName { get; set; }
        public string Level { get; set; }
        public DateTime AcademicYear { get; set; }

        public string RegId { get; set; }

        public int ClassId { get; set; }
        public decimal Pass { get; set; }
        //public IEnumerable<ClassIdPercentage> ClassPercentage { get; set; }
        public IEnumerable<ClassPercentageViewModel> ClassPercentageViewModel { get; set; }
    }
}