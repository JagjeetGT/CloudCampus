using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Setup
{
    public class FiscalYearViewModel :BaseViewModel
    {
     
        public FiscalYear FiscalYear { get; set; }
        public string DisplayStartDate { get; set; }
        public string DisplayEndDate { get; set; }
    }

    public class AcademicYearViewModel : BaseViewModel
    {

        public AcademicYear AcademyYear { get; set; }
        public string DisplayStartDate { get; set; }
        public string DisplayEndDate { get; set; }
    }
}