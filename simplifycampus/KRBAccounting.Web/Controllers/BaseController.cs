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
    //[CheckFirstInstallation]
    //[CheckModulePermissionAttribute("Academy")]
    [CheckAcademyYear]
    public class BaseController : Controller
    {
        private readonly DataContext _context = new DataContext();
        private string reportTitle = string.Empty;
        private string reportDate = string.Empty;
        private readonly HtmlViewRenderer htmlViewRenderer;
        private readonly StandardPdfRenderer standardPdfRenderer;
        private readonly IFormsAuthenticationService _authentication;
     

        public BaseController()
        {
            this.htmlViewRenderer = new HtmlViewRenderer();
            this.standardPdfRenderer = new StandardPdfRenderer();
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




        public T CreateReportViewModel<T>(string ReportHeader, string ReportDate) where T : ReportBaseViewModel, new()
        {
            reportTitle = ReportHeader;
            reportDate = ReportDate;
            var viewModelT = new T
            {
                ReportHeader = ReportBase,
                FYId = FiscalYear.Id,
                 SystemControl = SystemControl,
                AcademicYear = AcademicYear.StartDate.Year.ToString()
                
            };
            return viewModelT;
        }
        public T CreateReportViewModel<T>(string ReportHeader) where T : ReportBaseViewModel, new()
        {
            reportTitle = ReportHeader;
            var viewModelT = new T
            {

                ReportHeader = ReportBase,
                FYId = FiscalYear.Id,
                AcademicYear = AcademicYear.StartDate.Year.ToString(),
                SystemControl = SystemControl
            };
            return viewModelT;
        }
       
        public T CreateReportViewModel<T>() where T : ReportBaseViewModel, new()
        {

            var year = AcademicYear.StartDate.Year.ToString();
           if(SystemControl.DateType ==2)
           {
               year = NepaliDateService.NepaliDate(AcademicYear.StartDate).Year.ToString();
           }
          
            var viewModelT = new T
            {

                ReportHeader = ReportBase,
                FYId = FiscalYear.Id,
                AcademicYear = year,
                 SystemControl = SystemControl
            };
            return viewModelT;
        }
        public ReportHeader ReportBase
        {

            get
            {

                var model = FiscalYear;
                var company = CompanyInfo;
                var logo = CompanyInfo.LogoGuid + "_th" + CompanyInfo.LogoExt;
                var filePath = Server.MapPath("~/Content/Logo/" + logo);
                if (!System.IO.File.Exists(filePath))
                {
                    logo = "NoImage.jpg";
                }





                var reportModel = new ReportHeader()
                                      {
                                          AccountingPeriod = SystemControl.DateType==2? model.StartDateNep + " - " +
                                              model.EndDateNep:
                                              model.StartDate.ToString("dd/MM/yyyy") + " - " +
                                              model.EndDate.ToString("dd/MM/yyyy"),
                                          CompanyAddress =
                                              company.Address + ", " + company.City + ", " + company.State + ", " +
                                              company.Country,
                                          CompanyName = company.Name,
                                          ReportDate = reportDate,
                                          ReportTitle = reportTitle,
                                          PanNo = company.PanNo,
                                          Phone = company.PhoneNo,
                                          Email = company.Email,
                                     
                                          Logo = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~")) + "Content/Logo/" + logo

                                      };


                return reportModel;
            }
        }

        protected ActionResult ViewPdf(string pageTitle, string viewName, object model)
        {
            // Render the view html to a string.
            string htmlText = this.htmlViewRenderer.RenderViewToString(this, viewName, model);

            // Let the html be rendered into a PDF document through iTextSharp.
            byte[] buffer = standardPdfRenderer.Render(htmlText, pageTitle, null, null);

            // Return the PDF as a binary stream to the client.
            return new BinaryContentResult(buffer, "application/pdf");
            //return null;
        }
        //protected ActionResult ViewPdf(string pageTitle, string viewName, object model, Rectangle pageSize = null, List<FloatWidth> floatWidths = null)
        //{
        //    // Render the view html to a string.
        //    string htmlText = this.htmlViewRenderer.RenderViewToString(this, viewName, model);

        //    // Let the html be rendered into a PDF document through iTextSharp.
        //    //pixel per inch = 72
        //    //multiply the desired paper size(inch) by 72
        //    //width,height
        //    //Rectangle pageSize = new iTextSharp.text.Rectangle(320, 216);
        //    byte[] buffer = standardPdfRenderer.Render(htmlText, pageTitle, pageSize, floatWidths,10,10);

        //    // Return the PDF as a binary stream to the client.
        //    return new BinaryContentResult(buffer, "application/pdf");
        //    //return null;
        //}
        protected ActionResult ViewPdf(string pageTitle, string viewName, object model, Rectangle pageSize = null, List<FloatWidth> floatWidths = null, int HorizontalMargin=10, int VerticalMargin=10)
        {
            // Render the view html to a string.
            string htmlText = this.htmlViewRenderer.RenderViewToString(this, viewName, model);
            
            // Let the html be rendered into a PDF document through iTextSharp.
            //pixel per inch = 72
            //multiply the desired paper size(inch) by 72
            //width,height
            //Rectangle pageSize = new iTextSharp.text.Rectangle(320, 216);
            byte[] buffer = standardPdfRenderer.Render(htmlText, pageTitle, pageSize, floatWidths, HorizontalMargin, VerticalMargin);

            // Return the PDF as a binary stream to the client.
            return new BinaryContentResult(buffer, "application/pdf");
            //return null;
        }
        protected ActionResult ViewExcel(string pageTitle, string viewName, object model)
        {
            // Render the view html to a string.
            string htmlText = this.htmlViewRenderer.RenderViewToString(this, viewName, model);

            // Let the html be rendered into a PDF document through iTextSharp.
            //byte[] buffer = standardPdfRenderer.Render(htmlText, pageTitle);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(htmlText);

            Response.Clear();

            Response.ClearHeaders();

            Response.ClearContent();
            Response.Charset = "utf-8";
            Response.Buffer = true;
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "application/vnd.ms-excel";
            var guid = Guid.NewGuid().ToString();
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + guid + ".xls"));

            Response.BinaryWrite(buffer);

            Response.Flush();
            Response.Close();
            //Response.End();

            // Return the PDF as a binary stream to the client.
            //return new BinaryContentResult(buffer, "application/pdf");
            return null;
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

        public void UpdateDocumentNumbering(string name)
        {
            DataContext _datacontext = new DataContext();
            var documentNumbering = _datacontext.DocumentNumberingSchemes.FirstOrDefault(x => x.ModuleName == name);
            var number = string.Empty;
            if (documentNumbering == null) return;
            _datacontext.DocumentNumberingSchemes.Attach(documentNumbering);
            documentNumbering.DocCurrNo += 1;
            _datacontext.Entry(documentNumbering).State = EntityState.Modified;
            _datacontext.SaveChanges();
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
