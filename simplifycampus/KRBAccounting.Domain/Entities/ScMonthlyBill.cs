using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
   public  class ScMonthlyBill
    {
       [Key]
       public int Id { get; set; }
       public int FeeItemId { get; set; }
       public int MonthlyBillStudentId { get; set; }
       public decimal FeeAmount { get; set; }
       public decimal EducationTaxAmount { get; set; }
       [ForeignKey("FeeItemId")]
       public virtual ScFeeItem FeeItem { get; set; }
         [ForeignKey("MonthlyBillStudentId")]
       public virtual ScMonthlyBillStudent ScMonthlyBillStudent { get; set; } 

    }
}
