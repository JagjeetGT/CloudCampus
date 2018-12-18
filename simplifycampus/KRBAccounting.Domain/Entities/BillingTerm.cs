using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class BillingTerm
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("CheckCodeInBillingTerm", "Master", AdditionalFields = "Id,Type")]
        public int Code { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("CheckDescriptionInBillingTerm", "Master", AdditionalFields = "Id,Type")]
        [RegularExpression("^[0-9a-zA-Z ]+$", ErrorMessage = "Only AlphaNumeric")]
        public string Description { get; set; }
        [Required]
        public int GeneralLedgerId { get; set; }
        public int Category { get; set; }
        public int Basis { get; set; }
        public int Sign { get; set; }
        public bool IsProductWise { get; set; }
        public bool IsProfitability { get; set; }
        public bool SupressIfZero { get; set; }
        public int? TermBasis { get; set; }
        public decimal Rate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedById { get; set; }
        public string Type { get; set; }
        public int BranchId { get; set; }
        public bool IsActive { get; set; }

        //CheckBillingTermDisplayOrder
        [Remote("CheckBillingTermDisplayOrder", "Master", AdditionalFields = "Id")]
        public int DispalyOrder { get; set; }
        [ForeignKey("BranchId")]
        public CompanyInfo Branch { get; set; }

        public bool IsDeleted { get; set; }
    }
}
