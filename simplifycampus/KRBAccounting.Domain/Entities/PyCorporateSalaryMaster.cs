using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class PyCorporateSalaryMaster
    {
        [Key]
        public int Id { get; set; }
        public int EmployeePostId { get; set; }
        public decimal Salary { get; set; }
        public int FiscalYearId { get; set; }
        public int BranchId { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual User User { get; set; }
     [ForeignKey("EmployeePostId")]
        public virtual ScEmployeePost EmployeePost { get; set; }


            [NotMapped]
        public SelectList PositionList { get; set; }
        [NotMapped]
        public SelectList ALlowanceList { get; set; }

        [NotMapped]
        public List<int> AllowancesId { get; set; }

        public virtual ICollection<PyCorporateAllowanceMapping> CorporateAllowanceMappings { get; set; } 

    }
}
