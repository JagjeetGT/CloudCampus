using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Service.Helpers;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class ReportStudentLedgerFromViewModel : BaseViewModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public bool Summary { get; set; }
        public bool MonthlyBillDetial { get; set; }
        public bool SelectAll { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> SectionList { get; set; }
        public List<IGrouping<int, ScStudentRegistrationDetail>> StudentinfoList { get; set; }
        public int HouseId { get; set; }
        public string MitiFrom { get; set; }
        public string MitiTo { get; set; }
        public string DisplayDateFrom { get; set; }
        public string DisplayDateTo { get; set; }


    }

    public class ReportStudentBioDataViewModel : BaseViewModel
    {
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int StudentId { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> SectionList { get; set; }
        public List<SelectListItem> StudentList { get; set; }
    }

    public class ReportStudentCertificateViewModel : BaseViewModel
    {
        public int ClassId { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public int Certificatetype { get; set; }
        public SelectList CertificateTypeList { get; set; }
        public List<SelectListItem> StudentList { get; set; }
        public int StudentId { get; set; }
        public bool SelectAll { get; set; }
    }



}