using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.Academy
{
    public class AcademyViewModel : BaseViewModel
    {
       
        public int TotalStudent { get; set; }
        public List<string> AcademicYear { get; set; }
        public List<int> AcademicStudent { get; set; }
        public decimal TotalMaleStudent { get; set; }
        public decimal TotalFemaleStudent { get; set; }
        public List<string> ClassList { get; set; }
        public IEnumerable<ClassChartViewModel> ClassChart { get; set; }
        public IEnumerable<CategoryChartViewModel> CategoryChart { get; set; }
        public List<FeeAmountViewModel> FeeAmount { get; set; }
        public List<string> DivisionName { get; set; }
        public List<string> DivisionValue { get; set; }

        

        
    }
}