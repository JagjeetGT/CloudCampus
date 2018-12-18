using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class CashBankVoucherViewModel
    {
        public PagedList<CashBankMaster> AcceptedList { get; set; }
        public PagedList<CashBankMaster> PendingList { get; set; }
    }
}