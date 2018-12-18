using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class EntryControlInventory
    {
        public int Id { get; set; }
        public bool CostCenter { get; set; }
        public bool CostCenterItem { get; set; }
        public bool Godown { get; set; }
        public bool GodownItem { get; set; }
        public bool Class { get; set; }
        public bool Unit { get; set; }
        public bool AltUnit { get; set; }
        public bool AutoDesc { get; set; }
        public bool Remarks { get; set; }

        public bool GodownReqd { get; set; }
        public bool ClassReqd { get; set; }
        public bool RemarksReqd { get; set; }
    }
}
