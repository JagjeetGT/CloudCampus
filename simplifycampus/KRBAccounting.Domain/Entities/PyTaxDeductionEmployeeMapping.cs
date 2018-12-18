using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
   public class PyTaxDeductionEmployeeMapping
    {
       [Key]
       public int Id { get; set; }
       public int EmployeeId{ get; set; }
       public int TaxDeductionId { get; set; }
       [ForeignKey("EmployeeId")]
       public virtual ScEmployeeInfo EmployeeInfo { get; set; }
       [ForeignKey("TaxDeductionId")]
       public virtual PyTaxDeductionPattern PyTaxDeductionPattern { get; set; }
    }
}
