using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScBoader
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("AcademyBoaderCodeExists", "School", AdditionalFields = "Id")]
        public string Code { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("AcademyBoaderDescriptionExists", "School", AdditionalFields = "Id")]
        public string Description { get; set; }
        [Required(ErrorMessage = " ")]
        public int FeeItemId { get; set; }
        public string Memo { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedById { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = " ")]
        public int DispalyOrder { get; set; }
        [ForeignKey("FeeItemId")]
        public virtual ScFeeItem FeeItem { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }
        [ForeignKey("UpdatedById")]
        public virtual User UpdatedBy { get; set; }
    }
}
