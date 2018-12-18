using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_UnpaidLoanDashBoard
    {
        public int AccountId { get; set; }
        public string AccountHolderName { get; set; }
        public DateTime InstallmentDates { get; set; }
        public decimal InstallmentToPay { get; set; }
       
    }
}
