using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Service;
using KRBAccounting.Service.Models;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.Helpers;
using KRBAccounting.Web.Models;
using KRBAccounting.Web.ViewModels;
using ReportManagement;
using KRBAccounting.Web.CustomFilters;
using iTextSharp.text;
using BaseModel = KRBAccounting.Service.Models.BaseModel;

namespace KRBAccounting.Web.Controllers
{

    public class SearchBaseController : Controller
    {
        private readonly DataContext _context = new DataContext();
       

        private readonly IFormsAuthenticationService _authentication;


        public SearchBaseController()
        {
          
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
     
        
        }

        public CompanyInfo CurrentBranch
        {
            get
            {
                if (_authentication != null)
                {
                    var branchId = (_authentication.GetAuthenticatedUser() != null) ? _authentication.GetAuthenticatedUser().LastAccessedBranch:
                    0;
                    var branch = _context.CompanyInfos.FirstOrDefault(x => x.Id == branchId);
                   
                    if(branch ==null)
                    {
                        return _context.CompanyInfos.FirstOrDefault();
                    }
                    return branch;
                }
                return new CompanyInfo();
            }
            
        }
        public T CreateViewModel<T>(string module = "") where T : BaseViewModel, new()
        {
            var viewModelT = new T
            {
                SystemControl = SystemControl,
                CompanyInfo = CompanyInfo,
                FiscalYear = FiscalYear,
                UserRight = UserRight(module)
            };
            return viewModelT;
        }

        public T CreateModel<T>(string module = "") where T : BaseModel, new()
        {
            var viewModelT = new T
            {
                SystemControl = SystemControl,
                CompanyInfo = CompanyInfo,
                FiscalYear = FiscalYear,
                UserRight = UserRight(module)
            };
            return viewModelT;
        }



        public CompanyInfo CompanyInfo
        {
            get
            {
                var companyInfo = new CompanyInfo();
                if (_authentication.GetAuthenticatedUser() != null)
                {
                    if (_authentication.GetAuthenticatedUser().Username == "admin")
                        companyInfo = _context.CompanyInfos.FirstOrDefault(x => x.ParentId == 0);
                    else
                    {
                        var companyId = _authentication.GetAuthenticatedUser().CompanyId;
                        companyInfo = _context.CompanyInfos.FirstOrDefault(x => x.Id == companyId);
                    }
                }
                return companyInfo;
            }
        }

        public SystemControl SystemControl
        {
            get
            {
                var companyInfo = CompanyInfo;
                var systemControl = _context.SystemControls.FirstOrDefault(x => x.CompanyId == companyInfo.Id);
                return systemControl;
            }
        }
        public FiscalYear FiscalYear
        {
            get
            {
                var companyInfo = CompanyInfo;

                var fiscalyear = _context.FiscalYears.FirstOrDefault(x => x.IsDefalut && x.CompanyId == companyInfo.Id);
                return fiscalyear;
            }
        }

        public AcademicYear AcademicYear
        {
            get
            {
                var companyInfo = CompanyInfo;

                var fiscalyear = _context.AcademicYears.FirstOrDefault(x => x.IsDefalut && x.CompanyId == companyInfo.Id);

                return fiscalyear;
            }
        }




        
      
    

      

        public string RenderPartialViewToString()
        {
            return RenderPartialViewToString(null, null);
        }

        public string RenderPartialViewToString(string viewName)
        {
            return RenderPartialViewToString(viewName, null);
        }

        public string RenderPartialViewToString(object model)
        {
            return RenderPartialViewToString(null, model);
        }

        public string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        
        public UserRight UserRight(string module = "")
        {
            //get
            //{
            var userRight = new UserRight();
            if (User.IsInRole("admin"))
            {
                userRight.Approve = true;
                userRight.Create = true;
                userRight.Delete = true;
                userRight.Edit = true;
                userRight.Navigate = true;
            }
            var user = _authentication.GetAuthenticatedUser();
            var roles = user.Roles;

            foreach (var item in roles)
            {
                var securityRight = (from m in _context.ModuleLists.Where(x => x.ShortName == module)
                                     join sr in _context.SecurityRights.Where(x => x.Role == item.Id) on m.Id equals sr.ModuleId
                                     select sr).FirstOrDefault();
                if (securityRight != null)
                {
                    if (securityRight.Navigate)
                        userRight.Navigate = true;
                    if (securityRight.Create)
                        userRight.Create = true;
                    if (securityRight.Edit)
                        userRight.Edit = true;
                    if (securityRight.Delete)
                        userRight.Delete = true;
                    if (securityRight.Approve)
                        userRight.Approve = true;
                }

            }

            return userRight;
            //}
        }


    }
}
