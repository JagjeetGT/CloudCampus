using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Script.Serialization;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Service.Helpers;

namespace KRBAccounting.Web.ViewModels.Payroll
{
    public class PayrollGenerationViewModel : ReportBaseViewModel
    {
        public int Year { get; set; }
        public SelectList MonthList { get; set; }
        public List<CheckBoxListInfo> AnnualList { get; set; }
        public int Month { get; set; }
        public List<string> AnnualId { get; set; }
        
        [ScriptIgnore]
        public IEnumerable<PayrollGenerationAddViewModel> PayrollGenerations { get; set; }
        public IEnumerable<IGrouping<int, PayrollGenerationAddViewModel>> GroupingPayrollGenerations { get; set; }
        public IEnumerable<ScSalaryHead> SalaryHeadList { get; set; }
    }
}