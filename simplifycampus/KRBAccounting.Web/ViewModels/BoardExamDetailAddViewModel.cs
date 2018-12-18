using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels
{
    public class BoardExamDetailAddViewModel
    {
        public IEnumerable<GetSubjectByStudentIdandClassId> Subjects { get; set; }
        public IEnumerable<BoardExamDetail> BoardExamDetails { get; set; }
        public string Durationtype { get; set; }




    }
}