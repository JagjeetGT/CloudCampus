using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KRBAccounting.Web.ViewModels
{
    public class ReRegistrationViewModel
    {
        public int ClassId { get; set; }
        public List<SelectListItem> ClassList { get; set; }
      

    }
    
}