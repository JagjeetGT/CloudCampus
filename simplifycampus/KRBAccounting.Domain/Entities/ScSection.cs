using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScSection
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = " ")]
        [Remote("AcademySectionDescriptionExists","School",AdditionalFields = "Id")]
        public string Description { get; set; }

        public string Memo { get; set; }
    }
}
