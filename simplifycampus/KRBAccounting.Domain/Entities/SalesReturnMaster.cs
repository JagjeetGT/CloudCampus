using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class SalesReturnMaster
    {
        public int Id { get; set; }

        [Remote("CheckInvoiceNoInSalesReturnMaster", "Entry", AdditionalFields = "Id")]
        [Required(ErrorMessage = " ")]
        public string InvoiceNo { get; set; }
        [Required(ErrorMessage = " ")]
        public DateTime InvoiceDate { get; set; }
        public string InvoiceMiti { get; set; }
        [Required(ErrorMessage = " ")]
        public string RefBillNo { get; set; }
        [Required(ErrorMessage = " ")]
        public DateTime? RefBillDate { get; set; }
        public string RefBillMiti { get; set; }
        [Required(ErrorMessage = " ")]
        public int GlCode { get; set; }
        public int? AgentCode { get; set; }
        public string CurrCode { get; set; }
        public decimal? CurrRate { get; set; }
        public decimal BasicAmt { get; set; }
        public decimal? TermAmt { get; set; }
        public decimal NetAmt { get; set; }
        public string Remarks { get; set; }
        public int? SlCode { get; set; }
        public string PartyName { get; set; }
        public string ChequeNo { get; set; }
        public DateTime? ChequeDate { get; set; }
        public string VatNo { get; set; }
        public bool Posting { get; set; }
        public bool Export { get; set; }
        public DateTime? Reconcile { get; set; }
        public string BillSys { get; set; }
        public string TaxGroupDesc { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public int FYId { get; set; }
        public virtual ICollection<SalesReturnDetail> SalesReturnDetails { get; set; }
        public int BranchId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("BranchId")]
        public CompanyInfo Branch { get; set; }

        [NotMapped]
        public int? CurrencyId { get; set; }
    }
}
