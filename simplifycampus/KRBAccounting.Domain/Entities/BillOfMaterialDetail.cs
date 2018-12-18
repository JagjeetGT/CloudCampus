using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class BillOfMaterialDetail
    {
        [Key]
        public int Id { get; set; }
        public int BillOfMaterialId { get; set; }
        public int RawMaterialId { get; set; }
        public int? CostCenterId { get; set; }
        public int? GodownId { get; set; }
        public int UnitId { get; set; }
        public decimal Quantity { get; set; }

        [ForeignKey("BillOfMaterialId")]
        public virtual BillOfMaterial BillOfMaterial { get; set; }

        [ForeignKey("RawMaterialId")]
        public virtual Product RawMaterial { get; set; }

        [ForeignKey("UnitId")]
        public virtual Unit Unit { get; set; }
    }
}
