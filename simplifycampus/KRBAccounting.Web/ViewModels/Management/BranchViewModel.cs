using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Management
{
    public class BranchViewModel
    {
        public CompanyInfo Branch { get; set; }
        public string DisplayDate { get; set; }
        public SystemControl SystemControl { get; set; }
    }
}