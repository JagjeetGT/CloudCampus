using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScEmployeePost
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public string Description { get; set; }
        public int EmployeeCategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedById { get; set; }
        public bool Status { get; set; }
        [ForeignKey("UpdatedById")]
        public virtual User UpdatedBy { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }
        [ForeignKey("EmployeeCategoryId")]
        public virtual ScEmployeeCategory EmployeeCategory { get; set; }

        [NotMapped]
        public SelectList CategoryList { get; set; }
    }
}
