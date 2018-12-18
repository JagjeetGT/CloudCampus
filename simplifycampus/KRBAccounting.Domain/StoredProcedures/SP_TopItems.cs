using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_TopItems
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}
