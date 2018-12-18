using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class PyEmployeeSalaryAllowanceMapping
    {
        [Key]
        public int Id { get; set; }
        public int AllowanceId { get; set; }
        public int EmployeeSalaryId { get; set; }
        [ForeignKey("EmployeeSalaryId")]
        public virtual PyEmployeeSalaryMaster EmployeeSalaryMaster { get; set; }
        [ForeignKey("AllowanceId")]
        public virtual PyAllowanceMaster AllowanceMaster { get; set; }
    }
}
