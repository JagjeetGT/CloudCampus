using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_TrialBalanceAcGroupWiseAsOnDate
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int ASGId { get; set; }
        public string ASGDesc { get; set; }
        public int GlCode { get; set; }
        public string ShortName { get; set; }
        public string AccountName { get; set; }
        public int SLCode { get; set; }
        public string SLShortName { get; set; }
        public string SLAccountName { get; set; }
        public decimal LedBalance { get; set; }
        public decimal DebitCredit { get; set; }

    }
}
