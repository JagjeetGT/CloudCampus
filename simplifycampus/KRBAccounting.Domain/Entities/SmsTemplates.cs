using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class SmsTemplates
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        [Remote("CheckTemplates", "Sms", AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*")]
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
    }
}
