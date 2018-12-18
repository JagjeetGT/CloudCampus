using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class PyPFEmployeeMapping
    {
        [Key]
        public int Id { get; set; }
        public int PFEmployeeId { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual ScEmployeeInfo EmployeeInfo { get; set; }
        [ForeignKey("PFEmployeeId")]
        public virtual PyPFEmployeeMaster PyPfEmployeeMaster { get; set; }


    }
}
