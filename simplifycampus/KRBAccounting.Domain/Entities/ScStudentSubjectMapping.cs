using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScStudentSubjectMapping
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public int ClassId { get; set; }
        [Required(ErrorMessage = " ")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = " ")]
        public int SubjectId { get; set; }
        public int SubjectType { get; set; }
        public int ClassType { get; set; }
        public int ResultSystem { get; set; }
        public int EvaluateForType { get; set; }
        public string Narration { get; set; }
        public int AcademicYearId { get; set; }
       

        [ForeignKey("ClassId")]
        public virtual SchClass Class { get; set; }

        [ForeignKey("SubjectId")]
        public virtual ScSubject Subject { get; set; }

        [ForeignKey("StudentId")]
        public virtual ScStudentinfo Studentinfo { get; set; }

        [NotMapped]
        public int OldClassId { get; set; }
        [NotMapped]
        public int OldStudentId { get; set; }

        [NotMapped]
        public string Section { get; set; }

        [NotMapped]
        public string Regno { get; set; }
        [NotMapped]
        public string StdCode { get; set; }
    }
}