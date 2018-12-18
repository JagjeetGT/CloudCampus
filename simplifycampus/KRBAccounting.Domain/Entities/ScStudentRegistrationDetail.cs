using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScStudentRegistrationDetail
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = " ")]
        public int SectionId { get; set; }
        public int CurrentStatus { get; set; }
        public int ShiftId { get; set; }
        public int BoaderId { get; set; }
        public int StudentType { get; set; }
        public string Narration { get; set; }
        public string RollNo { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = " ")]
        public int RegMasterId { get; set; }
        [ForeignKey("StudentId")]
        public virtual ScStudentinfo Studentinfo { get; set; }

        [ForeignKey("RegMasterId")]
        public virtual ScStudentRegistration StudentRegistration { get; set; }
        [ForeignKey("SectionId")]
        public virtual ScSection Section { get; set; }
        [ForeignKey("ShiftId")]
        public virtual ScShift Shift { get; set; }
        [ForeignKey("BoaderId")]
        public virtual ScBoader Boader { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [NotMapped]
        public bool Checkbox { get; set; }

        [NotMapped]
        public bool[] CheckList { get; set; }

        [NotMapped]
        public DateTime[] Date { get; set; }

        [NotMapped]
        public string OldRollNo { get; set; }

        [NotMapped]
        public int OldSectionId { get; set; }
        [NotMapped]
        public int DeviceUserId { get; set; }
    }
}
