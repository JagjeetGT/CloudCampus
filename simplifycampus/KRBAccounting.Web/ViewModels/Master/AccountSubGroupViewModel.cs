using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Master
{
    public class AccountSubGroupViewModel
    {
        public AccountSubGroup AccountSubGroup { get; set; }

        public SelectList GroupList { get; set; }
    }
}