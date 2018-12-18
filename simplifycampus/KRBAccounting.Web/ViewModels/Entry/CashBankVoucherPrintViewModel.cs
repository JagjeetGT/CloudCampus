using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class CashBankVoucherPrintViewModel : ReportBaseViewModel
    {
        public CashBankMaster CashBankMaster { get; set; }
    }
}