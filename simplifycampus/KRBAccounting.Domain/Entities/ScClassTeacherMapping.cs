using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScClassTeacherMapping
    {
        [Key]
        public int Id { get; set; }

        public int ClassId { get; set; }
        public int TeacherId { get; set; }
        public int SectionId { get; set; }
        public bool IsClassTeacher { get; set; }
        [ForeignKey("ClassId")]
        public virtual SchClass Class { get; set; }

        [ForeignKey("SectionId")]
        public virtual ScSection Section { get; set; }

        [ForeignKey("TeacherId")]
        public virtual ScEmployeeInfo ScEmployeeInfo { get; set; }

        [NotMapped]
        public string TeacherName { get; set; }
    }
}
