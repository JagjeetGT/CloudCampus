using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Service.Models;

namespace KRBAccounting.Web.ViewModels
{
    public class BaseViewModel
    {
        public SystemControl SystemControl { get; set; }
        public CompanyInfo CompanyInfo { get; set; }
        public FiscalYear FiscalYear { get; set; }
        public AcademicYear AcademicYear { get; set; }
        public UserRight UserRight { get; set; }
        public SelectList BranchList { get; set; }
        public bool CheckBranch { get; set; }
        public int? BranchId { get; set; }
    }
}