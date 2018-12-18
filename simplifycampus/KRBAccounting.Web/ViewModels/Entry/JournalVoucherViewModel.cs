using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class JournalVoucherViewModel
    {
        public PagedList<JournalVoucher> AcceptedList { get; set; }
        public PagedList<JournalVoucher> PendingList { get; set; }
    }
}