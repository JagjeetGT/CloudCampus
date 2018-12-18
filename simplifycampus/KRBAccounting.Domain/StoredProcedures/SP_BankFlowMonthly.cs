using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
   public class SP_BankFlowMonthly
    {
       public int Id { get; set; }
       public int NoOfMonth { get; set; }
       public decimal Total { get; set; }
    }
}
