using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ExpiryBreakageMaster
    {
        [Key]
        [Required(ErrorMessage = " ")]
        public int Id { get; set; }
        public string ExpBrkNo { get; set; }
        public DateTime Date { get; set; }
        public string Miti { get; set; }
        public int ClassCode { get; set; }
        public string Type { get; set; }
        public string BrCode { get; set; }
        public string Remarks { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int BranchId { get; set; }
        public int FYId { get; set; }
        public decimal? CurRate { get; set; }
        public string CurCode { get; set; }
        public virtual ICollection<ExpiryBreakageDetail> ExpiryBreakageDetails { get; set; }
    
    }
}
