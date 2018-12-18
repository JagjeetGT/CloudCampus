using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class PurchaseInvoice
    {
        [Key]
        public int Id { get; set; }

        [Remote("CheckInvoiceNoInPurchaseInvoice", "Entry", AdditionalFields = "Id")]
        [Required(ErrorMessage = " ")]
        public string InvoiceNo { get; set; }
    
        [Required(ErrorMessage = " ")]
        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; }
        public string InvoiceMiti { get; set; }
        public string PartyInvoiceNo { get; set; }
        public DateTime? PartyInvoiceDate { get; set; }
        public string PartyInvoiceMiti { get; set; }
        public string PurchaseInvoiceType { get; set; }
        [Required(ErrorMessage = " ")]
        public int GlCode { get; set; }
        public int? SlCode { get; set; }
        public int? DueDay { get; set; }
        public DateTime? DueDate { get; set; }
        public string DueMiti { get; set; }
        public int? AgentCode { get; set; }
        public string CurrCode { get; set; }
        public decimal? CurrRate { get; set; }
        public decimal BasicAmt { get; set; }
        public decimal TermAmt { get; set; }
        public decimal NetAmt { get; set; }
        public string Remarks { get; set; }

        public int CreatedById { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public int FYId { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User User { get; set; }
        [ForeignKey("GlCode")]
        public virtual Ledger Ledger { get; set; }
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
        public int BranchId { get; set; }

        public int? ChallanId { get; set; }

        [ForeignKey("BranchId")]
        public CompanyInfo Branch { get; set; }
        public int? DocId { get; set; }
        public bool IsDeleted { get; set; }
        public int? OrderId { get; set; }
        public string OrderNo { get; set; }

        [NotMapped]
        public int? CurrencyId { get; set; }
    }
}
