using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScClassFeeRate
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public int ClassId { get; set; }
        [Required(ErrorMessage = " ")]
        public int FeeItemId { get; set; }
        [Required(ErrorMessage = " ")]
        public decimal FeeRate { get; set; }
        [Required(ErrorMessage = " ")]
        public int BoaderId { get; set; }
        [Required(ErrorMessage = " ")]
        public int ShiftId { get; set; }
        public string Narr { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedById { get; set; }
        public int AcademyYearId { get; set; }
        [ForeignKey("FeeItemId")]
        public virtual ScFeeItem ScFeeItem { get; set; }
        [ForeignKey("ClassId")]
        public virtual SchClass Classes { get; set; }
        [ForeignKey("ShiftId")]
        public virtual ScShift Shifts { get; set; }
        [ForeignKey("BoaderId")]
        public virtual ScBoader Boaders { get; set; }

        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }
        [ForeignKey("UpdatedById")]
        public virtual User UpdatedBy { get; set; }
    }
}
