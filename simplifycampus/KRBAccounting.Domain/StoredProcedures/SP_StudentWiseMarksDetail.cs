using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
   public class SP_StudentWiseMarksDetail
    {
        public int SubjectId { get; set; }
        public string Code { get; set; }
        public string SubDesc { get; set; }
        public int ResultSystem { get; set; }
        public int EvaluateForType { get; set; }
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
