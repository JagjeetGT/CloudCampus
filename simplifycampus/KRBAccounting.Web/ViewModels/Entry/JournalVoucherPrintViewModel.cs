using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class JournalVoucherPrintViewModel : ReportBaseViewModel
    {
        public JournalVoucher JournalVoucher { get; set; }
    }
}