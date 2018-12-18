using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScDivision
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = " ")]
        [Remote("AcademyDivisionDescriptionExists","School",AdditionalFields = "Id")]
        public string Description { get; set; }

        [RegularExpression(@"^[0-9]+(\.[0-9][0-9]?)?$", ErrorMessage = "Please Insert Numeric Only.")]
        public decimal PercentageFrom { get; set; }

        [RegularExpression(@"^[0-9]+(\.[0-9][0-9]?)?$", ErrorMessage = "Please Insert Numeric Only.")]
        public decimal PercentageTo { get; set; }

        public string Memo { get; set; }
        public int Schedule { get; set; }
    }
}
