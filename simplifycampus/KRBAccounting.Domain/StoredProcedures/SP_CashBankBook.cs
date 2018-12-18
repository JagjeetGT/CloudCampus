using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_CashBankBook
    {
        public int Id { get; set; }
        public string VoucherNo { get; set; }
        public string AccountName { get; set; }
        public string ShortName { get; set; }
        public int LedgerId { get; set; }
        public decimal Receive { get; set; }
        public decimal Payment { get; set; }
        public int AmountType { get; set; }
        public string SubLedName { get; set; }
        public string Remarks { get; set; }
        public string Narration { get; set; }
        public string ChequeNo { get; set; }
        public string ChequeDate { get; set; }
        public string ChequeMiti { get; set; }
    }
}
