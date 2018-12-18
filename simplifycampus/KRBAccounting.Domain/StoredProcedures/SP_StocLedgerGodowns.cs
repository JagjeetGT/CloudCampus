using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_StocLedgerGodowns
    {
        public string Godown { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
    }
}
