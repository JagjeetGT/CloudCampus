using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScExamMarkSetup
    {
        [Key]
        public int Id { get; set; }
        public int ExamId { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int ResultSystem { get; set; }
        public int EvaluateForType { get; set; }
        public Decimal PracticalFullMark { get; set; }
        public Decimal PracticalPassMark { get; set; }
        public Decimal TheoryFullMark { get; set; }
        public Decimal TheoryPassMark { get; set; }
        public string Narration { get; set; }

        
        public int SubjectType { get; set; }
        public int ClassType { get; set; }

        [NotMapped]
        public decimal Total { get; set; }


        [ForeignKey("ExamId")]
        public virtual ScExam ScExam { get; set; }
        [ForeignKey("ClassId")]
        public virtual SchClass Class { get; set; }
        [ForeignKey("SubjectId")]
        public virtual ScSubject Subject { get; set; }

    }
}
