using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Master
{
    public class UdfAddViewModel
    {
        public UDFEntry UdfEntry { get; set; }
        public SelectList ControlType { get; set; }

        public SelectList EntryModule { get; set; }
    }
}