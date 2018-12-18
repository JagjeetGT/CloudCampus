using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class CashBankMaster
    {
        [Key]
        public int Id { get; set; }

        [Remote("CheckVoucherNoInEntry","Entry",AdditionalFields="Id")]
        [Required(ErrorMessage = " ")]
        public string VoucherNo { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = " ")]
        public DateTime VoucherDate { get; set; }
        public string VoucherMiti { get; set; }
        
       // [Required(ErrorMessage = " ")]
        public int LedgerId { get; set; }

        public decimal? CurrentBalance { get; set; }

        public string ChequeNo { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ChequeDate { get; set; }
        public string ChequeMiti { get; set; }

        public int? CurrencyId { get; set; }

        //Currency Rate
        public decimal? Rate { get; set; }

        public string Remarks { get; set; }

        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedById { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public int? DocId { get; set; }
        public int FYid { get; set; } // Fiscal Year
        public bool IsDeleted { get; set; }

        [ForeignKey("FYid")]
        public virtual FiscalYear FiscalYear { get; set; }

        [ForeignKey("LedgerId")]
        public virtual Ledger Ledger { get; set; }

        public virtual ICollection<CashBankDetail> CashBankDetails { get; set; }
        public int BranchId { get; set; }

        [ForeignKey("BranchId")]
        public virtual CompanyInfo Branch { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }
        [NotMapped]
        public string CurrencyName { get; set; }
  
        
    }
}
