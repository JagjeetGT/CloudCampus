using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class Ledger
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Account Name")]
        [Required(ErrorMessage = " ")]
        [Remote("CheckAccountNameInLedger", "Master", AdditionalFields = "Id")]
        public string AccountName { get; set; }

        [Display(Name = "Short Name")]
        [Required(ErrorMessage = " ")]
        [Remote("CheckShortNameInLedger", "Master", AdditionalFields = "Id")]
        public string ShortName { get; set; }

        [Display(Name="Group")]
        [Required(ErrorMessage = "Account Group Required")]
        public int? AccountGroupId { get; set; }

        [Display(Name = "Sub-Group")]
        public int? AccountSubGroupId { get; set; }
        
        public bool IsCashBank { get; set; }
        public bool IsCashBook { get; set; }
        public bool IsSubLedger { get; set; }
        public bool IsBillWiseAdjustment { get; set; }
        public string Category { get; set; }
        public int? AreaId { get; set; }
        public int? AgentId { get; set; }
        public int? CurrencyId { get; set; }
        public decimal? CreditLimit { get; set; }
        public int? CreditDays { get; set; }
        public int? ChequeReceiptDays { get; set; }
        public int? Scheme { get; set; }
        public decimal? RateOfInterest { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PhoneO { get; set; }
        public string PhoneR { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string PanNo { get; set; }
        public string DLNo { get; set; }
        public string ContactPerson { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedById { get; set; }
        public int BranchId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("BranchId")]
        public CompanyInfo Branch { get; set; }
    }
}
