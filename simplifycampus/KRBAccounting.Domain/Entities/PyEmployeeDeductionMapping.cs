using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class PyEmployeeDeductionMapping
    {
        [Key]
        public int Id { get; set; }
        public int DeductionId { get; set; }
        public int EmployeeDeductionId { get; set; }
        [ForeignKey("DeductionId")]
        public virtual PyDeductionMaster DeductionMaster { get; set; }
        [ForeignKey("EmployeeDeductionId")]
        public virtual PyEmployeeDeductionMaster EmployeeDeductionMaster { get; set; }
    }
}
