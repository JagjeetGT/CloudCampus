using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class EntryControlPurchase
    {
        [Key]
        public int Id { get; set; }
        public bool SubLedger { get; set; }
        public bool Agent { get; set; }
        public bool Godown { get; set; }
        public bool Class { get; set; }
        public bool Unit { get; set; }
        public bool AltUnit { get; set; }
        public bool Order { get; set; }
        public bool Challan { get; set; }
        public bool Currency { get; set; }
        public bool BasicAmount { get; set; }
        public bool AutoDesc { get; set; }
        public bool Remarks { get; set; }
        public bool OrderReqd { get; set; }
        public bool ChallanReqd { get; set; }
        public bool GodownReqd { get; set; }
        public bool ClassReqd { get; set; }
        public bool CurrencyReqd { get; set; }
        public bool SubLedgerReqd { get; set; }
        public bool AgentReqd { get; set; }
        public bool RemarksReqd { get; set; }

        public bool ChangeRate { get; set; }
    }
}
