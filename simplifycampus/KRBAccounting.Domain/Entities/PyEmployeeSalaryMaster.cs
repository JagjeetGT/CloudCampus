using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class PyEmployeeSalaryMaster
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal BasicSalary { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int BranchId { get; set; }
        public int FiscalYearId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual ScEmployeeInfo EmployeeInfo { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual User User { get; set; }

        public virtual ICollection<PyEmployeeSalaryAllowanceMapping> AllowanceSalaryAllowanceMappings { get; set; } 
    }
}
