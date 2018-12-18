using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class PurchaseChallanMaster
    {
        [Key]
        public int Id { get; set; }
        [Remote("CheckChallanNoInPurchaseInvoice", "Entry", AdditionalFields = "Id")]
        [Required(ErrorMessage = " ")]
        public string ChallanNo { get; set; }
        public DateTime ChallanDate { get; set; }
        public string QNo { get; set; }
        public DateTime? QDate { get; set; }
        public int GlCode { get; set; }
        public int AgentCode { get; set; }
        public int ClassCode { get; set; }
        public int CurrencyCode { get; set; }
        public decimal CurrencyRate { get; set; }
        public decimal BasicAmt { get; set; }
        public decimal TermAmt { get; set; }
        public decimal NetAmt { get; set; }
        public string Remarks { get; set; }
        public string BrCode { get; set; }
        public string PartyName { get; set; }
        public string ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string VatNo { get; set; }
        public string ChallanMiti { get; set; }
        public int? DocId { get; set; }
        public int BranchId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int FYId { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        [ForeignKey("GlCode")]
        public virtual Ledger Ledger { get; set; }

        public int? OrderId { get; set; }
        public string OrderNo { get; set; }
        public virtual ICollection<PurchaseChallanDetail> PurchaseChallanDetails { get; set; }

        [ForeignKey("CreatedById")]
        public virtual User User { get; set; }

        [NotMapped]
        public int? CurrencyId { get; set; }
    }
}
