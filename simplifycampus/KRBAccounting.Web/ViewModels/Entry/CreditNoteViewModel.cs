using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class CreditNoteViewModel
    {
        public PagedList<CreditNoteMaster> AcceptedList { get; set; }
        public PagedList<CreditNoteMaster> PendingList { get; set; }
    }
}