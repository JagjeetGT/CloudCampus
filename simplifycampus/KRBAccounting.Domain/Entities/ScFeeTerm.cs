using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Domain.Entities
{
    public class ScFeeTerm
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Remote("AcademyFeeTermCodeExists", "School", AdditionalFields = "Id")]
        public string Code { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("AcademyFeeTermDescriptionExists", "School", AdditionalFields = "Id")]
        public string Description { get; set; }
        [Required(ErrorMessage = " ")]
        public int GLCode { get; set; }
        [Required(ErrorMessage = " ")]
        public int Category { get; set; }
        [Required(ErrorMessage = " ")]
        public int Rounded { get; set; }
        [Required(ErrorMessage = " ")]
        public int Sign { get; set; }
        [Required(ErrorMessage = " ")]
        public int Type { get; set; }
        [Required(ErrorMessage = " ")]
        public decimal Rate { get; set; }
        public string Formula { get; set; }
        public bool SupressZero { get; set; }
        public bool Profitablity { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedById { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = " ")]
        public int DispalyOrder { get; set; }

        [ForeignKey("GLCode")]
        public virtual Ledger Ledger { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }
        [ForeignKey("UpdatedById")]
        public virtual User UpdatedBy { get; set; }
        [NotMapped]
        public SelectList Categories { get; set; }
        [NotMapped]
        public SelectList FeeTermTypes { get; set; }
        [NotMapped]
        public SelectList Signs { get; set; }
        [NotMapped]
        public SelectList RoundedOffTypes { get; set; }
    }
}
