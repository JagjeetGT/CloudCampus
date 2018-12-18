using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Master
{
    public class AgentAddViewModel
    {
        public Agent Agent { get; set; }

        public SelectList LedgerList { get; set; }

        public SelectList SubLedgerList { get; set; }
    }
}