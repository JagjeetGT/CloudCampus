using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
   public class ConsolidateDetail
    {

        [Key]
        public int Id { get; set; }
        public int ConsolidateId { get; set; }
        public int ExamId { get; set; }
        public int SubjectId { get; set; }

        public decimal FullMarks { get; set; }
        public decimal PassMarks { get; set; }
        public decimal ObtainedMarks { get; set; }
        public decimal TheoryFullMark { get; set; }
        public decimal PracticalFullMark { get; set; }
        public decimal PracticalPassMark { get; set; }
        public decimal TheoryObtainedMark { get; set; }
        public decimal PracticalObtainedMark { get; set; }
        public decimal HighestMarks { get; set; }
        [ForeignKey("SubjectId")]
        public virtual ScSubject Subject { get; set; }
        [ForeignKey("ExamId")]
        public virtual ScExam Exam { get; set; }
        [ForeignKey("ConsolidateId")]
        public virtual Consolidate Consolidated { get; set; }
    }
}
