using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScExamRoutine
    {
        [Key]
        public int Id { get; set; }
        public int ExamId { get; set; }
        
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int ResultSystem { get; set; }
        public int EvaluateForType { get; set; }
        public int ExamType { get; set; }
        public DateTime? ExamDate { get; set; }
        public string ExamMiti { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ExamHour { get; set; }
        public string Remarks { get; set; }
        public int AcademicYearId { get; set; }

        [ForeignKey("ExamId")]
        public virtual ScExam ScExam { get; set; }
        [ForeignKey("ClassId")]
        public virtual SchClass Class { get; set; }
        [ForeignKey("SubjectId")]
        public virtual ScSubject Subject { get; set; }

    }
}
