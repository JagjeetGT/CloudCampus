using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_ReOrderStatus
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public decimal? ReOrderLevel { get; set; }
        public decimal? ReOrderQty { get; set; }
        public decimal? CurrentStock { get; set; }
        public decimal? MinimumStock { get; set; }
        public decimal? MaximumStock { get; set; }
    }
}
