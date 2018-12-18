using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class Sp_SubjectWiseMarksDetail
    {
        public int StudentID { get; set; }
        public string StdCode { get; set; }
        public string StuDesc { get; set; }
        public string RollNo { get; set; }
        public decimal PracticalFullMark { get; set; }
        public decimal PracticalObtainedMarks { get; set; }
        public decimal TheoryObtainedMarks { get; set; }
        public int TheoryStatus { get; set; }
        public int PracticalStatus { get; set; }
        public decimal PracticalPassMark { get; set; }
        public decimal TheoryFullMark { get; set; }
        public decimal TheoryPassMark { get; set; }
        public int SubjectType { get; set; }
        public decimal Total { get; set; }
        public string Narration { get; set; }
        public int ClassType { get; set; }

 
    
    }
}
