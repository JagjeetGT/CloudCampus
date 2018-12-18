using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class SalesInvoice
    {
        [Key]
        public int Id { get; set; }

        [Remote("CheckInvoiceNoInSalesInvoice", "Entry", AdditionalFields = "Id")]
        [Required(ErrorMessage = " ")]
        public string InvoiceNo { get; set; }
        [Required(ErrorMessage = " ")]
        public DateTime InvoiceDate { get; set; }
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
        public decimal TenderAmt { get; set; }
        public decimal ReturnAmt { get; set; }
        public string Remarks { get; set; }
        public string BrCode { get; set; }
        public string BillSys { get; set; }
        public string PartyName { get; set; }
        public string ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string VatNo { get; set; }
        public string SalesOrder { get; set; }
        public string SalesChallan { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EffectiveDate { get; set; }
        public bool AuditLock { get; set; }
        public string InvoiceMiti { get; set; }
        public bool Posting { get; set; }
        public bool Export { get; set; }
        public string QuotNo { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Reconcile { get; set; }
        public bool Suspend { get; set; }
        public string SourceModule { get; set; }
        public bool IsDeleted { get; set; }

        public int? ChallanId { get; set; }
        public int CreatedById { get; set; }
        public int? OrderId { get; set; }
        public string OrderNo { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public int FYId { get; set; }

        [ForeignKey("GlCode")]
        public virtual Ledger Ledger { get; set; }

        [ForeignKey("CreatedById")]
        public virtual User User { get; set; }
        public virtual ICollection<SalesDetail> SalesDetails { get; set; }
        public int BranchId { get; set; }

        [ForeignKey("BranchId")]
        public CompanyInfo Branch { get; set; }

        public int? DocId { get; set; }

        [NotMapped]
       public int? CurrencyId { get; set; }


    }
}