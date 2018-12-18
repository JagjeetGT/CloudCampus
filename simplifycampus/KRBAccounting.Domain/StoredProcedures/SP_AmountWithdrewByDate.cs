using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_AmountWithdrewByDate
    {
        public DateTime WithdrawnOn { get; set; }
        public int ChequeNumber { get; set; }
        public decimal WithdrawnAmount { get; set; }
        public int AccountId { get; set; }
        public string AccNo { get; set; }
        public string AccountHolderName { get; set; }
        public string WithdrawnBy { get; set; } 
        public string Remarks { get; set; }
    }
}
