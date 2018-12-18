using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class MaterialReturn
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public string Code { get; set; }
        [Required(ErrorMessage = " ")]
        public DateTime ReturnDate { get; set; }
        public string ReturnMiti { get; set; }
        [Required(ErrorMessage = " ")]
        public DateTime IssueDate { get; set; }
        public string IssueMiti { get; set; }
        [Required(ErrorMessage = " ")]
        public int MaterialIssueId { get; set; }
        [Required(ErrorMessage = " ")]
        public int CostCenterId { get; set; }
        [Required(ErrorMessage = " ")]
        public int BranchId { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int FYId { get; set; }
        public string Remarks { get; set; }

        [ForeignKey("MaterialIssueId")]
        public virtual MaterialIssue MaterialIssue { get; set; }
        [ForeignKey("CostCenterId")]
        public virtual CostCenter CostCenter { get; set; }

        public virtual ICollection<MaterialReturnDetail> MaterialReturnDetails { get; set; }
    }
}
