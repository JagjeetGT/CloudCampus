using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class Godown
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("CheckNameInGodown", "Master", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("CheckShortNameInGodown", "Master", AdditionalFields = "Id")]
        public string ShortName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
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
