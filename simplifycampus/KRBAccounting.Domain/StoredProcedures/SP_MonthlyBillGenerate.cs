using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
   public class SP_MonthlyBillGenerate
    {
       public int Id { get; set; }
       public string StudentName { get; set; }
       public string Section { get; set; }
       public string RollNo { get; set; }
       public string Status { get; set; }
       public decimal Amount { get; set; }
       public decimal EducationTax { get; set; }
       public int StudentId { get; set; }
    }
}
