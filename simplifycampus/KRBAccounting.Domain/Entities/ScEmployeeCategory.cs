using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Domain.Entities
{
    public class ScEmployeeCategory
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("AcademyEmployeeCategoryCodeExists", "EmployeeManagement", AdditionalFields = "Id")]
        public string Code { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("AcademyEmployeeCategoryDescriptionExists", "EmployeeManagement", AdditionalFields = "Id")]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedById { get; set; }
        public bool Status { get; set; }
        public string Remarks { get; set; }
        [ForeignKey("UpdatedById")]
        public virtual User UpdatedBy { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }
    
    }
}
