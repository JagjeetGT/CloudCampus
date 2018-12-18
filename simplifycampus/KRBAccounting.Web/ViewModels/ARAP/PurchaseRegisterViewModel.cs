using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.ARAP
{
    public class PurchaseRegisterViewModel : ReportBaseViewModel
    {
        public IEnumerable<SP_PurchaseVatRegister> PurchaseRegisters { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int DateType { get; set; }
    }
}