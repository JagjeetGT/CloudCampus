using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScClassSubjectMapping
    {
        [Key]
        public int Id { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public int SubjectType { get; set; }
        public int ClassType { get; set; }
        public int ResultSystem { get; set; }
        public int EvaluateForType { get; set; }
        public string Narration { get; set; }

        [ForeignKey("ClassId")]
        public virtual SchClass Class { get; set; }
        [ForeignKey("SubjectId")]
        public virtual ScSubject Subject { get; set; }

        [NotMapped]
        public ScClassSchedule Schedule { get; set; } 
    }
}