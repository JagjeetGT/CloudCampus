using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class EntryControlPL
    {
        [Key]
        public int Id { get; set; }
        public bool Class { get; set; }
        public bool ClassItem { get; set; }
        public bool Currency { get; set; }
        public bool SubLedger { get; set; }
        public bool Agent { get; set; }
        public bool Remarks { get; set; }
        public bool ClassReqd { get; set; }
        public bool CurrencyReqd { get; set; }
        public bool SubLedgerReqd { get; set; }
        public bool AgentReqd { get; set; }
        public bool RemarksReqd { get; set; }
    }
}
