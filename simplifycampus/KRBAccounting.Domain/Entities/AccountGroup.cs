using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class AccountGroup
    {
        [Key]
        public int Id { get; set; }

        //[Required(ErrorMessage = "*")]
        public string Code { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("CheckInDescription", "Master", AdditionalFields = "Id")]
        public string Description { get; set; }
        public string GroupType { get; set; }
        public string GroupAccountType { get; set; }
        public int DisplayOrder { get; set; }
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
