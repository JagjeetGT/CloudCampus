using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Domain.Entities
{
    public class ScStudentRegistration
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public int ClassId { get; set; }

        public int AcademicYearId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CreatedById { get; set; }
        public int UpdatedById { get; set; }
        public string Remarks { get; set; }
        [ForeignKey("ClassId")]
        public virtual SchClass Class { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }
        [ForeignKey("UpdatedById")]
        public virtual User UpdatedBy { get; set; }

        public virtual ICollection<ScStudentRegistrationDetail>  StudentRegistrationDetails { get; set; }
            [NotMapped]
        public IEnumerable<ScStudentRegistrationDetail> RegistrationDetails { get; set; }
   
    }
}
