using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_PLPeriodicLedgers
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string ShortName { get; set; }
        public decimal OpeningTotal { get; set; }
        public decimal PeriodTotal { get; set; }
        public decimal Closing { get; set; }
        public int ASGId { get; set; }
        public string ASGName { get; set; }
        public string SLName { get; set; }
    }
}
