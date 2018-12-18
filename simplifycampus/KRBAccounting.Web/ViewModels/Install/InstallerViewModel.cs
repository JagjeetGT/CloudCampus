using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Install
{
    public class InstallerViewModel
    {
      
        public string CompanyInitial { get; set; }
        [Required(ErrorMessage = " ")]
        public string CompanyName { get; set; }
        public string CompanyCountry { get; set; }
        public string CompanyState { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyAddress { get; set; }

        [Display(Name = "Phone No")]
        public string CompanyPhoneNo { get; set; }
        public string CompanyFax { get; set; }
        public string CompanyEmail { get; set; }

        [Display(Name = "Pan No")]
        public string CompanyPanNo { get; set; }
        [Display(Name = "Tax Invoice")]
        public bool CompanyTaxInvoice { get; set; }

        [Display(Name = "Start Date")]
        public DateTime CompanyStartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime CompanyEndDate { get; set; }

        public string CompanyStartMiti { get; set; }
        public string CompanyEndMiti { get; set; }
        public string DisplayCompanyStartDate { get; set; }
        public string DisplayCompanyEndDate { get; set; }
        public int ParentId { get; set; }
        public string LogoGuid { get; set; }
        public string LogoExt { get; set; }

        public AdminInfo AdminInfo { get; set; }
        public DatabaseInfo DatabaseInfo { get; set; }
        public string DisplayFYStartDate { get; set; }
        public string DisplayFYEndDate { get; set; }
        public string FYStartDateNep { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime FYStartDate { get; set; }

      
        public string FYEndDateNep { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime FYEndDate { get; set; }
        public int CompanyId { get; set; }
        public bool IsDefalut { get; set; }

        public int CreatedById { get; set; }
        public int UpdatedById { get; set; }

        [Required]
        public DateTime AcademicStartDate { get; set; }
       
        public string AcademicStartMiti { get; set; }
        [Required]
        public DateTime AcademicEndDate { get; set; }
        
        public string AcademicEndMiti { get; set; }

        public string AcademicStartDisplayDate { get; set; }
        public string AcademicEndDisplayDate { get; set; }
    }
}