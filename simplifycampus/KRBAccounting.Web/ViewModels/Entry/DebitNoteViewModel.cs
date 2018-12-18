using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Entry
{
    public class DebitNoteViewModel
    {
        public PagedList<DebitNoteMaster> AcceptedList { get; set; }
        public PagedList<DebitNoteMaster> PendingList { get; set; }
    }
}