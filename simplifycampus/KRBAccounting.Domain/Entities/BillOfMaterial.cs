using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class BillOfMaterial
    {
        [Key]
        public int Id { get; set; }
       
        public string Code { get; set; }
        [Required(ErrorMessage=" ")]
        public string Description { get; set; }
        public int CostCenterId { get; set; }
        public int UnitId { get; set; }
        public decimal  Qty { get; set; }
        public int FinishedGoodId { get; set; }
        public decimal ConversionFactor { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public int BranchId { get; set; }
        public string Remarks { get; set; }
        ////public string CurrCode { get; set; }
        ////public decimal? CurrRate { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User CreatedUser { get; set; }
        public int FYId { get; set; }

        [ForeignKey("ModifiedById")]
        public virtual User ModifiedUser { get; set; }
        [ForeignKey("UnitId")]
        public virtual Unit Unit { get; set; }

        [ForeignKey("FinishedGoodId")]
        public virtual Product FinishedGood { get; set; }
        
        public virtual ICollection<BillOfMaterialDetail> BillOfMaterialDetails { get; set; }

        //[NotMapped]
        //public int CurrencyId { get; set; }
    }
}
