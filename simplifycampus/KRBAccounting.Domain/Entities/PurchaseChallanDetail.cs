using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class PurchaseChallanDetail
    {
        [Key]
        public int Id { get; set; }
        public int ChallanId { get; set; }
        public int SNo { get; set; }
        public int ProductCode { get; set; }
        public int? GodownCode { get; set; }
        public decimal? AltQty { get; set; }
        public string AltUnit { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; }
        public decimal? AltStockQty { get; set; }
        public decimal StockQty { get; set; }
        public decimal Rate { get; set; }
        public decimal BasicAmt { get; set; }
        public decimal TermAmt { get; set; }
        public decimal NetAmt { get; set; }
        public string Remarks { get; set; }

        [ForeignKey("ChallanId")]
        public virtual PurchaseChallanMaster PurchaseChallanMaster { get; set; }

        [NotMapped]
        public SelectList UnitList { get; set; }
    }
}
