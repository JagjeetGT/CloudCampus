using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScStudentExtraActivityDetails
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public int StudentId { get; set; }
        public int ActivitiesStatusId { get; set; }

        public int MasterId { get; set; }
         [Required(ErrorMessage = " ")]
        public DateTime StartDate { get; set; }
        public string StartMiti { get; set; }
        public string Narration { get; set; }

        [ForeignKey("StudentId")]
        public virtual ScStudentinfo Studentinfo { get; set; }
        [ForeignKey("MasterId")]
        public virtual ScStudentExtraActivity ScStudentExtraActivity { get; set; }
        [NotMapped]
        public string StdCode { get; set; }

        [NotMapped]
        public string RollNo { get; set; }
    }
}
