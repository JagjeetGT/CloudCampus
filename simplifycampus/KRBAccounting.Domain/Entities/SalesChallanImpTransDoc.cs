using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class SalesChallanImpTransDoc
    {
        [Key]
        public int Id { get; set; }
        public string PPDNo { get; set; }
        public DateTime PPDDate { get; set; }
        public decimal TaxableAmt { get; set; }
        public decimal Vat { get; set; }
        public string CustomName { get; set; }
        public string Transport { get; set; }
        public string VehicleNo { get; set; }
        public string CnNo { get; set; }
        public DateTime CnDate { get; set; }
        public string DocumentBank { get; set; }
        public int ChallanId { get; set; }
        public string PPDMiti { get; set; }
        public string CnMiti { get; set; }

        [ForeignKey("ChallanId")]
        public virtual SalesChallanMaster SalesChallanMaster { get; set; }
    }
}
