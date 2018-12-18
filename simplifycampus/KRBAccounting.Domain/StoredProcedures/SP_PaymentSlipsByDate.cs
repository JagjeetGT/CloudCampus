using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_PaymentSlipsByDate
    {
        public DateTime IssuedOn { get; set; }
        public int AccountId { get; set; }
        public string AccNo { get; set; }
        public string AccHolderName { get; set; }
        public string ChequeRanges { get; set; }
        public int TotalIssuedCheque { get; set; }
        public int FYId { get; set; }
    }

}
