using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScStudentFeeTerm
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        public int FeeTermId { get; set; }
        public decimal LocalRate { get; set; }
        public decimal LocalAmount { get; set; }
        [Required(ErrorMessage = "*")]
        public int DetailId { get; set; }
        public bool IsItemWise { get; set; }
        public int StudentFeeId { get; set; }
        public int? Index { get; set; }
        [ForeignKey("FeeTermId")]
        public virtual ScFeeTerm FeeTerm { get; set; }

        [ForeignKey("DetailId")]
        public virtual ScStudentFeeRateDetail StudentFeeRateDetail { get; set; }
        [ForeignKey("StudentFeeId")]
        public virtual ScStudentFeeRateMaster StudentFeeRateMaster { get; set; }


        [NotMapped]
        public string ParentGuid { get; set; }
    }
}
