using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels
{
    public class ReportBaseViewModel
    {
        public ReportHeader ReportHeader { get; set; }
        public int FYId { get; set; }
        public SystemControl SystemControl { get; set; }
        public string AcademicYear { get; set; }
    }

    public class ReportHeader
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string AccountingPeriod { get; set; }
        public string ReportTitle { get; set; }
        public string ReportDate { get; set; }
        public string PanNo { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public string Phone { get; set; }
      
    }
}