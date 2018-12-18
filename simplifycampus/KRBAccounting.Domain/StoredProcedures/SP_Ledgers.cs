using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_Ledgers
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string ShortName { get; set; }
        public int GroupById { get; set; }
        public string GroupByName { get; set; }
        public string GroupByShortName { get; set; }
        public int SubGroupById { get; set; }
        public string SubGroupByName { get; set; }
        public string SubGroupByShortName { get; set; }

    }
}
