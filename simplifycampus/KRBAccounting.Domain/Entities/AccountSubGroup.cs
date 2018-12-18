using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class AccountSubGroup
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("CheckInDescriptionMaster", "Master", AdditionalFields = "Id")]
        public string Description { get; set; }
        [Required(ErrorMessage = " Account Group Required")]
        public int? AccountGroupId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedById { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("AccountGroupId")]
        public virtual AccountGroup AccountGroup { get; set; }
    }
}
