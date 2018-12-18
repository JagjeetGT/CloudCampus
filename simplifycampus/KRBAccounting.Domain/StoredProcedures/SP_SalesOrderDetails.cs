using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_SalesOrderDetails
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNo { get; set; }
        public int GlCode { get; set; }
        public string AccountName { get; set; }
        public decimal NetAmt { get; set; }
        public string Remarks { get; set; }
        public string OrderMiti { get; set; }
        public int OrderId { get; set; }
        public int LedgerId { get; set; }
        public string Code { get; set; }
        public string ProductDecription { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal BasicAmount { get; set; }
        public int Orders { get; set; }
        public int SNo { get; set; }
    }
}
