using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_LoanAssignmentByDate
    {
        public DateTime LoanStartDate { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public decimal SanctionedAmount { get; set; }
        public string CollectorName { get; set; }
        public string LoanName { get; set; }    
    }
}
