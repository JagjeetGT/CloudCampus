using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScExtraActivity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("ExtraActivityDescriptionNoExists", "School", AdditionalFields = "Id")]
        public string Description { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("ExtraActivityCodeNoExists", "School", AdditionalFields = "Id")]
        public string Code { get; set; }
        public string Memo { get; set; }
    }
}
