using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_AccountWatchList
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public decimal Total { get; set; }
    }
}
