using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_JournalVourcherHeader
    {
        public DateTime VDate { get; set; }
        public string VMiti { get; set; }
        public string VNo { get; set; }
        public string Source { get; set; }
        public string Particulars { get; set; }
        public string Remarks { get; set; }
        public string UserFullName { get; set; }
    }
}
