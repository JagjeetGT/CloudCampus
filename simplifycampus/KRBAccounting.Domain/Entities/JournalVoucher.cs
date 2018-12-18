using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class JournalVoucher
    {
        [Key]
        public int Id { get; set; }

        [Remote("CheckJVNumberInJournalVoucher", "Entry", AdditionalFields = "Id")]
        [Required(ErrorMessage = " ")]
        public string JVNumber { get; set; }

        //[Required(ErrorMessage = "*")]
        public DateTime JVDate { get; set; }
        //[Required(ErrorMessage = "*")]
        public string JVMiti { get; set; }
        public string CurCode { get; set; }
        public decimal? CurRate { get; set; }
        public string Remarks { get; set; }
        public int? CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public int? FYId { get; set; }
        public int? DocId { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User Users { get; set; }
        public virtual ICollection<JournalVoucherDetail> JournalVoucherDetails { get; set; }
        public int? BranchId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("BranchId")]
        public virtual CompanyInfo Branch { get; set; }
        [NotMapped]
        public int CurrencyId { get; set; }
       
    }
}
