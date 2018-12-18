using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.ARAP
{
    public class SalesRegisterViewModel : ReportBaseViewModel
    {
        public IEnumerable<SP_SalesVatRegister> SalesRegisters { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int DateType { get; set; }
    }
}