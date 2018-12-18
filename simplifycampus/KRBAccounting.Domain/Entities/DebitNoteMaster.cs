using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class DebitNoteMaster
    {
        [Key]
        public int Id { get; set; }

        [Remote("CheckNumberInDebitNote", "Entry", AdditionalFields = "Id")]
        [Required(ErrorMessage = " ")]
        public string Number { get; set; }

        [Required(ErrorMessage = " ")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Miti { get; set; }
        [Required(ErrorMessage = " ")]
        public int GlCode { get; set; }

        public int? AgentCode { get; set; }

        public string CurCode { get; set; }

        public decimal? CurRate { get; set; }

        //SubLedger
        public int? SlCode { get; set; }
         [Required(ErrorMessage = " ")]
        public string RefBillNo { get; set; }
         [Required(ErrorMessage = " ")]
        public DateTime RefBillDate { get; set; }
        public string RefBillMiti { get; set; }
      
        public string Remarks { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public int FYId { get; set; }
        public int? DocId { get; set; }
        public virtual ICollection<DebitNoteDetail> DebitNoteDetails { get; set; }
        [ForeignKey("GlCode")]
        public virtual Ledger Ledgers { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User Users { get; set; }
        public int BranchId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("BranchId")]
        public CompanyInfo Branch { get; set; }

        [NotMapped]
        public int CurrencyId { get; set; }

        
    }
}
