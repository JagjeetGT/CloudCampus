using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_AccountStatement
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int AccountId { get; set; }
        public string VoucherNo { get; set; }
        public string Description { get; set; }
        public decimal DrAmt { get; set; }
        public decimal CrAmt { get; set; }
        public decimal Balance { get; set; }
    }
}
