using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScShift
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("AcademyShiftCodeExists", "School", AdditionalFields = "Id")]
        public string Code { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("AcademyShiftDescriptionExists", "School", AdditionalFields = "Id")]
        public string Description { get; set; }
        public string Memo { get; set; }
        [Required(ErrorMessage = " ")]
        public DateTime CreatedDate { get; set; }
        [Required(ErrorMessage = " ")]
        public int CreatedById { get; set; }
        [Required(ErrorMessage = "")]
        public DateTime UpdatedDate { get; set; }
        [Required(ErrorMessage = " ")]
        public int UpdatedById { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = " ")]
        public int DispalyOrder { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }
        [ForeignKey("UpdatedById")]
        public virtual User UpdatedBy { get; set; }
    }
}
