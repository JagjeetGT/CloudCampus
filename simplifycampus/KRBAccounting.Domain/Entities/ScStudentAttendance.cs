using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class ScStudentAttendance
{
        [Key]
        public int Id {get;set;}
        [Required(ErrorMessage =  "*" )]
        public int StudentId {get;set;}
        public DateTime Date {get;set;}
        public DateTime CreatedDate {get;set;}
        [Required(ErrorMessage =  "*" )]
        public string Status {get;set;}

        public int ClassId { get; set; }
        public int AcademicYearId { get; set; }
        public int SectionId { get; set; }
    [ForeignKey("ClassId")]
        public virtual SchClass Class { get; set; }
    [ForeignKey("SectionId")]
    public virtual ScSection Section { get; set; }
    [ForeignKey("StudentId")]
    public virtual ScStudentinfo Studentinfo { get; set; }

    [NotMapped]
    public string StudentName { get; set; }

    [NotMapped]
    public int PresentDay { get; set; }

    }
}
