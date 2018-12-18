using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
   public  class SP_EmployeeSalaryCreditSlipHeader
    {
       public int Id { get; set; }
       public string Code { get; set; }
       public string EName { get; set; }
       public string EAddress { get; set; }
       public DateTime DateOfJoin { get; set; }
       public string MitiofJoin { get; set; }
       public string EPost { get; set; }
       public string Department { get; set; }
       public string EType { get; set; }
    }

}
