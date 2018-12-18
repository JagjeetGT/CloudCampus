using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using KRBAccounting.Data;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.Helpers;

namespace KRBAccounting.Web.CustomFilters
{
    public class CheckAcademyYear : ActionFilterAttribute
    {
        private readonly DataContext _context = new DataContext();

        private readonly IFormsAuthenticationService _authentication;
        public CheckAcademyYear()
        {
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = _authentication.GetAuthenticatedUser();
            if (user != null)
            {
                var companyinfo = CompanyInfo;
                if (companyinfo.Id != 0)
                {
                    if (AcademicYear.EndDate.ToDate() < DateTime.UtcNow.ToDate())
                    {
                        filterContext.Result = _authentication.GetAuthenticatedUser().Username == "admin"
                                                   ? new RedirectResult("~/Error/SettingError")
                                                   : new RedirectResult("~/Error/SettingError");
                    }
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Error/Company");
                }
            }


        }
        public CompanyInfo CompanyInfo
        {
            get
            {
                var companyInfo = new CompanyInfo();

                var user = _authentication.GetAuthenticatedUser();
                if (user != null)
                {
                    if (_authentication.GetAuthenticatedUser().Username == "admin")
                        companyInfo = _context.CompanyInfos.FirstOrDefault(x => x.ParentId == 0);
                    else
                    {
                        var companyId = _authentication.GetAuthenticatedUser().CompanyId;
                        companyInfo = _context.CompanyInfos.FirstOrDefault(x => x.Id == companyId);
                    }
                }
                return companyInfo ?? (companyInfo = new CompanyInfo());
            }
        }


        public AcademicYear AcademicYear
        {
            get
            {
                var fiscalyear = new AcademicYear();
                var companyInfo = CompanyInfo;

                var user = _authentication.GetAuthenticatedUser();
                if (user != null)
                    fiscalyear = _context.AcademicYears.FirstOrDefault(x => x.IsDefalut && x.CompanyId == companyInfo.Id);


                if (fiscalyear == null)
                {
                    fiscalyear = new AcademicYear();
                }

                return fiscalyear;
            }
        }

    }
}