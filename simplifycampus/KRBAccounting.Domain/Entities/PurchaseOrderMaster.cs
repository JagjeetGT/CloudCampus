using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class PurchaseOrderMaster
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public string OrderMiti { get; set; }
        [Remote("CheckPurchaseOrderInvoiceNo", "Entry", AdditionalFields = "Id")]
        [Required(ErrorMessage = " ")]
        public string OrderNo { get; set; }
        public string OANo { get; set; }
        public DateTime? OADate { get; set; }
        public int GlCode { get; set; }
        public int AgentCode { get; set; }
        public int ClassCode { get; set; }
        public string CurrencyCode { get; set; }
        public decimal CurrentyRate { get; set; }
        public decimal ProductAmt { get; set; }
        public decimal TermAmt { get; set; }
        public decimal NetAmt { get; set; }
        public decimal BasicAmt { get; set; }
        public string Remarks { get; set; }
        public string BrCode { get; set; }
        public string PartyName { get; set; }
        public string ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string VatNo { get; set; }
        public string PbQuoNo { get; set; }
        public string TaxGroup { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDeleted { get; set; }
        public int BranchId { get; set; }
        public int? DocId { get; set; }
        [ForeignKey("BranchId")]
        public CompanyInfo Branch { get; set; }
        public int CreatedById { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public int FYId { get; set; }
        [ForeignKey("CreatedById")]
        public virtual User User { get; set; }
        [ForeignKey("GlCode")]
        public virtual Ledger Ledger { get; set; }
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

        [NotMapped]
        public int? CurrencyId { get; set; }
    }
}