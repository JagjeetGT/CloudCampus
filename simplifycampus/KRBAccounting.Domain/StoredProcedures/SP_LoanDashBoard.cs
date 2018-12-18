using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_LoanDashBoard
    {
        public int MaturedLoans { get; set; }
        public int ExceededLoans { get; set; }
        public int ApprovedLoans { get; set; }
        public int PendingLoans { get; set; }
    }
}
