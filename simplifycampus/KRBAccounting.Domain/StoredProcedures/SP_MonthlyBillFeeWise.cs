using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
   public class SP_MonthlyBillFeeWise
    {
     
       public string StudentName { get; set; }
       public string Description { get; set; }
       public string Section { get; set; }
       public string RollNo { get; set; }
       public decimal Amount { get; set; }
       public decimal NetAmount { get; set; }
       public int StudentId { get; set; }
       public int FeeItemId { get; set; }
    }
}
