using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class MaterialIssue
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public string Code { get; set; }
        [Required(ErrorMessage = " ")]
        public string Description { get; set; }
        [Required(ErrorMessage = " ")]
        public DateTime IssueDate { get; set; }
        public string IssueMiti { get; set; }
        [Required(ErrorMessage = " ")]
        public int BillOfMaterialId { get; set; }
        public int UnitId { get; set; }
        public decimal Qty { get; set; }
        public int FinishedGoodId { get; set; }
        public decimal ConversionFactor { get; set; }
        [Required(ErrorMessage = " ")]
        public int CostCenterId { get; set; }
        [Required(ErrorMessage = " ")]
        public int BranchId { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string Remarks { get; set; }
        public int FYId { get; set; }
        [ForeignKey("CostCenterId")]
        public virtual CostCenter CostCenter { get; set; }
        [ForeignKey("BillOfMaterialId")]
        public virtual BillOfMaterial BillOfMaterial { get; set; }

        public virtual ICollection<MaterialIssueDetail> MaterialIssueDetails { get; set; }

        [ForeignKey("FinishedGoodId")]
        public virtual Product FinishedGood { get; set; }
        [ForeignKey("UnitId")]
        public virtual Unit Unit { get; set; }

        [NotMapped]
        public string DisplayDate { get; set; }
    }
}
