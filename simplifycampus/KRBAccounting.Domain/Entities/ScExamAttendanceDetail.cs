using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScExamAttendanceDetail
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        public int MasterId { get; set; }
        [Required(ErrorMessage = "*")]
        public int StudentId { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public string Narration { get; set; }

        [ForeignKey("StudentId")]
        public virtual ScStudentinfo Studentinfo { get; set; }

        [ForeignKey("MasterId")]
        public virtual ScExamAttendanceMaster ExamAttendanceMaster { get; set; }

        [NotMapped]
        public string StdCode { get; set; }
        [NotMapped]
        public string StdRollNo { get; set; }

        [NotMapped]
        public string ExamCode { get; set; }

        [NotMapped]
        [ForeignKey("StudentId")]
        public virtual ScStudentRegistration StudentRegistration { get; set; }
    }
}
