using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScExamAttendanceMaster
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public int ClassId { get; set; }
        [Required(ErrorMessage = " ")]
        public int SectionId { get; set; }
        [Required(ErrorMessage = " ")]
        public int ExamId { get; set; }
        [Required(ErrorMessage = " ")]
        public DateTime EntryDate { get; set; }
        [Required(ErrorMessage = " ")]
        public string EntryMiti { get; set; }

        [RegularExpression(@"^[0-9]+?$", ErrorMessage = " ")]
        [Required(ErrorMessage = " ")]
        public int AcademicYearId { get; set; }
        public int TotalDays { get; set; }
        public int UpdatedById { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }
        [ForeignKey("UpdatedById")]
        public virtual User UpdatedBy { get; set; }
        [ForeignKey("ClassId")]
        public virtual SchClass Classes { get; set; }
        [ForeignKey("SectionId")]
        public virtual ScSection Section { get; set; }
        [ForeignKey("ExamId")]
        public virtual ScExam Exam { get; set; }
        [NotMapped]
        public IEnumerable<ScExamAttendanceDetail> AttendanceDetails { get; set; }
        [NotMapped]
        public string date { get; set; }


        
    }
}
