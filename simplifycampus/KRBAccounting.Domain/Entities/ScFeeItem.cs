using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Domain.Entities
{
    public class ScFeeItem
    {
        [Key]
        public int Id { get; set; }

        [Remote("AcademyFeeItemCodeExists", "School", AdditionalFields = "Id")]
        [Required(ErrorMessage = " ")]
        public string Code { get; set; }

        [Required(ErrorMessage = " ")]
        public int GLCode { get; set; }

        [Required(ErrorMessage = " ")]
        [Remote("AcademyFeeItemDescriptionExists", "School", AdditionalFields = "Id")]
        public string Description { get; set; }

        [Required(ErrorMessage = " ")]
        public int Type { get; set; }

        public string Memo { get; set; }
         [Required(ErrorMessage = " ")]
        public int Schedule { get; set; }

         public bool EducationTax { get; set; }
        [ForeignKey("GLCode")]
        public virtual Ledger Ledger { get; set; }
    }
}
