using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Management
{
    public class CompanyViewModel
    {
        public CompanyInfo Company { get; set; }
        public string DisplayStartDate { get; set; }
        public string DisplayEndDate { get; set; }
        public IEnumerable<CompanyInfo> Branches { get; set; }
        public SystemControl SystemControl { get; set; }
    }
}