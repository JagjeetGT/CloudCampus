using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.ARAP
{
    public class SalesRegisterFormViewModel
    {
        [Required]
        public string StartDate { get; set; }
        [Required]
        public string EndDate { get; set; }
        [Required]
        public int VatTerm { get; set; }

        public int DateType { get; set; }
    }
}