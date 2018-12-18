using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScStudentFeeRateDetail
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public int MasterId { get; set; }
        [Required(ErrorMessage = " ")]
        public int FeeItemId { get; set; }
        public decimal FeeRate { get; set; }
        public decimal TermAmount { get; set; }
        public decimal NetAmount { get; set; }
        public string Narration { get; set; }
        public int Index { get; set; }
        [ForeignKey("FeeItemId")]
        public virtual ScFeeItem FeeItem { get; set; }
        [NotMapped]
        public int AllowFeeWiseFeeTerm { get; set; }
        [ForeignKey("MasterId")]
        public virtual ScStudentFeeRateMaster FeeRateMaster { get; set; }

        [NotMapped]
        public string DetailGuid { get; set; }

        [NotMapped]
        public string FeeTermName { get; set; }
  
     

    }
}
