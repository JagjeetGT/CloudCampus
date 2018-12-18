using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScStudentCategory
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("AcademyStudentCategoryCodeExists", "Student", AdditionalFields = "Id")]
        public string Code { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("AcademyStudentCategoryDescriptionExists", "Student", AdditionalFields = "Id")]
        public string Description { get; set; }
        public string Memo { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedById { get; set; }
 
        [ForeignKey("UpdatedById")]
        public virtual User UpdatedBy { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }

    }
}
