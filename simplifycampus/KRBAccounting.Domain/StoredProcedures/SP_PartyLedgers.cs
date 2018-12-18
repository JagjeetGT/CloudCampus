using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_PartyLedgers
    {
        public string VNo { get; set; }
        public DateTime VDate { get; set; }
        public string VMiti { get; set; }
        public string Source { get; set; }
        public int GlCode { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public string Narration { get; set; }
        public string ProductDetails { get; set; }
        public string TermDetails { get; set; }
        public string ChequeNo { get; set; }
        public DateTime ChequeDate { get; set; }
        public string ChequeMiti { get; set; }

    }
}
