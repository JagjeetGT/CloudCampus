using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_PLTotalForDashBoard
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public decimal ThisWeekTotal { get; set; }
        public decimal ThisMonthTotal { get; set; }
        public decimal ThisYearTotal { get; set; }
    }
}
