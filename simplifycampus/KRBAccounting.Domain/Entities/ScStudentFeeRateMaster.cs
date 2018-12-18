using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScStudentFeeRateMaster
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public int ClassId { get; set; }
        [Required(ErrorMessage = " ")]
        public int StudentId { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedById { get; set; }
        public decimal BasicAmount { get; set; }
        public decimal TermAmount { get; set; }
        public decimal NetAmount { get; set; }
        public int AcademicYearId { get; set; }
        [ForeignKey("ClassId")]
        public virtual SchClass Class { get; set; }
        [ForeignKey("StudentId")]
        public virtual ScStudentinfo Studentinfo { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }

        [ForeignKey("UpdatedById")]
        public virtual User UpdatedBy { get; set; }

        public virtual ICollection<ScStudentFeeRateDetail> ScStudentFeeRateDetails { get; set; }
        public virtual ICollection<ScStudentFeeTerm> StudentFeeTerms { get; set; } 

    }
}
