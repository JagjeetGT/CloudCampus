using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_SalesChallanSummary
    {

        public int Id { get; set; }
        public int ChallanId { get; set; }
        public DateTime ChallanDate { get; set; }
        public string ChallanMiti { get; set; }
        public string ChallanNo { get; set; }
        public int LedgerId { get; set; }
        public string Customer { get; set; }
        public decimal BasicAmount { get; set; }
        public string Remarks { get; set; }
        public int GlCode { get; set; }
        public string AccountName { get; set; }
        public decimal NetAmt { get; set; }
        public int OrderId { get; set; }
    }
}
