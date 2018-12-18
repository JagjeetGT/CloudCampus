using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_JournalVoucherDetail
    {
        public int Id { get; set; }
        public string JVNumber { get; set; }
        public int GlCode { get; set; }
        public decimal Amount { get; set; }
        public decimal DrAmt { get; set; }
        public decimal CrAmt { get; set; }
        public string Narration { get; set; }
        public string SubLedName { get; set; }
        public int JVMasterId { get; set; }
        public int JVAmountType { get; set; }
        public string AccountName { get; set; }
        public string ShortName { get; set; }
    }
}
