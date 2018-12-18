using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Web.CustomFilters;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.Services;
using KRBAccounting.Web.ViewModels;


namespace KRBAccounting.Web.Controllers
{
 //   [CheckFirstInstallation]
    public class HomeController : BaseController
    {
        private readonly ICompanyInfoRepository _company;
        private readonly IFormsAuthenticationService _authentication;
        private readonly IUserRepository _user;
        private readonly IDatabaseFactory _factory;
        private readonly ISystemControlRepository _systemControl;

        public HomeController(ICompanyInfoRepository companyInfoRepository,
            IUserRepository userRepository,
            IDatabaseFactory databaseFactory,
            ISystemControlRepository systemControlRepository)
        {
            _company = companyInfoRepository;
            _user = userRepository;
            _factory = databaseFactory;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _systemControl = systemControlRepository;
            //Gobals a = new Gobals();
            //a.fromAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));

        }
        //
        // GET: /Home/

        public ActionResult Index()
        {
            
            if (!Request.IsAuthenticated)
            {
                var logonViewModel = new LogOnModel();
                logonViewModel.EnableBranch = _systemControl.GetAll().FirstOrDefault().EnableBranch;
                if (!logonViewModel.EnableBranch)
                    logonViewModel.BranchId = _company.Filter(x => x.ParentId == 0).FirstOrDefault().Id;
                else
                    logonViewModel.BranchList = new SelectList(_company.GetAll(), "Id", "Name");
                return View("Logon", logonViewModel);
            }
            var company = _company.GetAll();
            if (!company.Any())
            {
                return View("CompanyCreate");
            }
           
            

            if (!User.IsInRole("admin"))
            {


                if (User.IsInRole("librarian"))
                {
                    return RedirectToAction("DashBoard", "Library");
                }

                if (User.IsInRole("student"))
                {
                    return RedirectToAction("Index", "StudentProfile");
                }
                if (User.IsInRole("teacher"))
                {
                    return RedirectToAction("Index", "Teacher");
                }
            }
            else
            {
                return RedirectToAction("DashBoard", "School");
            }

            return View();
        }
   
        [HttpPost]
        public ActionResult CompanyCreate(CompanyInfo model)
        {
            model.CreatedDate = DateTime.UtcNow;
            _company.Add(model);
            return RedirectToAction("Index");
        }

        




        public ActionResult MenuList(string username)
        {
            var categoryId = _authentication.GetCachedCategorId();
            var cached = username + "." + categoryId;
            var Cachecategory = HttpContext.Cache[cached] as string;
            if (Cachecategory == null)
            {
                Cachecategory = this.RenderPartialViewToString("_PartialMenu1",
                                                               MenuHelper.MenuList(username, categoryId));
               
              // MenuHelper.MenuList(username, categoryId);
           HttpContext.Cache[cached] = Cachecategory;
            }
            return PartialView("_PartialMenu2", Cachecategory);
        }
        //        [OutputCache(Duration = 3600, VaryByParam = "username; categoryId")]
        //public ActionResult CustomMenu(string username, int categoryId)
        //{
        //            //if(categoryId ==0)
        //            //{
        //            //    categoryId = Convert.ToInt32( Session["categoryId"]);
        //            //}
        //            //else
        //            //{
        //            //    Session["categoryId"] = categoryId;
        //            //}
                 

        //            switch (categoryId)
        //            {
        //                case (int)HeaderMenu.Accounting:
        //                    {
        //                        return RedirectToAction("DashBoard", "Entry");
        //                    }
        //                case (int)HeaderMenu.Academy:
        //                    {
        //                        return RedirectToAction("DashBoard", "School");
        //                    }
        //                case (int)HeaderMenu.Payroll:
        //                    {
        //                        return RedirectToAction("DashBoard", "Payroll");
        //                    }
        //                case (int)HeaderMenu.Inventory:
        //                    {
        //                        return RedirectToAction("DashBoard", "Inventory");
        //                    }
        //                case (int)HeaderMenu.SMS:
        //                    {
        //                        return RedirectToAction("DashBoard", "Sms");
        //                    }
        //                case (int)HeaderMenu.Library:
        //                    {
        //                        return RedirectToAction("DashBoard", "Library");
        //                    }
        //                case (int)HeaderMenu.Setting:
        //                    {
        //                        return RedirectToAction("DashBoard", "Setup");
        //                    }
        //                default:
        //                    {
        //                        return RedirectToAction("Index", "Home");
        //                    }
        //            }
            
        //}







    }
}
