using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_GetAmtFromAccTrans
    {
        public int AccountId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal MinDeposit { get; set; }
        public decimal ActualBalance { get; set; }
    }
}
