using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_CashBankBookSummary
    {
        public int Id { get; set; } // master Id
        public int GlCode { get; set; }
        public DateTime VDate { get; set; }
        public string VMiti { get; set; }
        public decimal Opening { get; set; }
        public decimal Receive { get; set; }
        public decimal Balance { get; set; }
        public decimal Payment { get; set; }

    }
}
