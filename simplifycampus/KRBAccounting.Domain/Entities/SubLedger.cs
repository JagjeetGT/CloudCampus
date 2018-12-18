using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class SubLedger
    {
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("CheckDescription", "Master", AdditionalFields = "Id")]
        public string Description { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("CheckShortName", "Master", AdditionalFields = "Id")]
        public string ShortName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNo { get; set; }
        public decimal? InterestRate { get; set; }
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
