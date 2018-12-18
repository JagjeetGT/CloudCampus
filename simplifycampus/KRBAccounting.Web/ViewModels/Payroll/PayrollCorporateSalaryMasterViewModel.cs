using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Service.Helpers;

namespace KRBAccounting.Web.ViewModels.Payroll
{
    public class PayrollCorporateSalaryMasterViewModel : PyCorporateSalaryMaster
    {
        public PyCorporateSalaryMaster CorporateSalaryMaster { get; set; }
        public List<CheckBoxListInfo> CheckBoxListInfos { get; set; }

    }
}