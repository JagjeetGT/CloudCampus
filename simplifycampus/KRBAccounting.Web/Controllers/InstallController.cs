using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using KRBAccounting.Data;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Service;
using KRBAccounting.Service.Helpers;
using KRBAccounting.Web.Helpers;
using KRBAccounting.Web.ViewModels;
using System.Text;
using KRBAccounting.Web.ViewModels.Install;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Configuration = System.Configuration.Configuration;

namespace KRBAccounting.Web.Controllers
{
    public class InstallController : Controller
    {
        private readonly FileHelper _filehelp = new FileHelper();
        public ActionResult Index()
        {
            var firstInstall = WebConfigHelper.ReadValue("FirstInstall");
            if (firstInstall != null)
            {
                if (firstInstall != "1")
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                WebConfigHelper.WriteSetting("FirstInstall", "1");
            }
            //FirstInstall
            return View();
        }

        [HttpPost]
        public ActionResult NewInstall(InstallerViewModel model)
        {
             // Date
            //model.CompanyStartMiti = model.DisplayCompanyStartDate;
            //model.CompanyStartDate = NepaliDateService.NepalitoEnglishDate(model.CompanyStartMiti);
            //model.CompanyEndMiti = model.DisplayCompanyEndDate;
            //model.CompanyEndDate = NepaliDateService.NepalitoEnglishDate(model.CompanyEndMiti);
            //model.AcademicEndMiti = model.AcademicEndDisplayDate;
            //model.AcademicEndDate = NepaliDateService.NepalitoEnglishDate(model.AcademicEndMiti);
            //model.AcademicStartMiti = model.AcademicStartDisplayDate;
            //model.AcademicStartDate = NepaliDateService.NepalitoEnglishDate(model.AcademicStartMiti);
            //model.FYEndDateNep = model.DisplayFYStartDate;
            //model.FYEndDate = NepaliDateService.NepalitoEnglishDate(model.FYEndDateNep);
            //model.FYStartDateNep = model.DisplayFYStartDate;
            //model.FYStartDate = NepaliDateService.NepalitoEnglishDate(model.FYStartDateNep);

            if (ModelState.IsValid)
            {
                string prefix = "CO";
                string DatabaseName = string.Empty;
                string msg = string.Empty;
                string dbAuthorization = string.Empty;

                //Integrated Security=SSPI
                if (string.IsNullOrEmpty(model.DatabaseInfo.DbUserName))
                {
                    dbAuthorization = "Integrated Security=SSPI";
                }
                else
                {
                    dbAuthorization = "User ID=" + model.DatabaseInfo.DbUserName + ";Password=" +
                                      model.DatabaseInfo.DbPassword;
                }
                string connString = "Data Source=" + model.DatabaseInfo.ServerName + ";Initial Catalog =Master;" + dbAuthorization;
                try
                {
                    DatabaseName = InstallHelper.AddDataBase(connString, prefix);
                }
                catch (Exception ex)
                {
                    msg = "Please Insert Valid Database Info";
                    return Json(new { success = false, msg = msg });
                }
                //ConfigurnewConnectionString(model.DatabaseInfo.ServerName, DatabaseName, model.DatabaseInfo.DbUserName, model.DatabaseInfo.DbPassword);

                var str = "Use " + DatabaseName + ";";
                str += System.IO.File.ReadAllText(Server.MapPath("~/Content/assets/db/script.sql"));
                str += ";";
                string sqlConnectionString = connString;
                SqlConnection conn = new SqlConnection(sqlConnectionString);
                Server server = new Server(new ServerConnection(conn));
                //Company
                InstallHelper.AddCompanyInfo(ref str, model);
                //User
                InstallHelper.AddUserInfo(ref str, model.AdminInfo);
                //Role
                InstallHelper.AddRole(ref str);
                //Role Mapping
                InstallHelper.AddRoleMapping(ref str);
                //System Control
                InstallHelper.AddSystemControl(ref str);
                //Fiscal Year
                InstallHelper.AddFiscalYear(ref str, model.FYStartDateNep, model.FYEndDateNep, model.FYStartDate, model.FYEndDate);
                //Academic Year
                InstallHelper.AddAcademicYear(ref str, model.AcademicStartMiti, model.AcademicEndMiti, model.AcademicStartDate,
                                              model.AcademicEndDate);
                //Default Data
                str += System.IO.File.ReadAllText(Server.MapPath("~/Content/assets/db/default_script.sql"));
                str += ";";
                //Run Alter Script
                string searchPattern = "alter_*.txt";  // This would be for you to construct your prefix
                DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/Content/assets/db/"));
                FileInfo[] files = di.GetFiles(searchPattern);
                foreach (var item in files)
                {
                    str += System.IO.File.ReadAllText(item.FullName);
                    str += ";";
                }

                server.ConnectionContext.ExecuteNonQuery(str);
                //Change FirstInstall in appKey to false
                WebConfigHelper.UpdateAppConfigValue("FirstInstall", "0");
                WebConfigHelper.ConfigureNewConnectionString(model.DatabaseInfo.ServerName, DatabaseName, dbAuthorization);
                return Json(new { success = true });
            }
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                if (modelState.Errors.Any())
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                    }
                }
            }
            return Json(new { success = false, msg = "please fill all the required datas" });
        }
        [HttpPost]
        public ActionResult UploadImages(IEnumerable<HttpPostedFileBase> attachments)
        {
            var logoGuid = string.Empty;
            var fileName = string.Empty;
            var fileext = string.Empty;
            var filenamewithoutext = string.Empty;
            foreach (var file in attachments)
            {
                fileext = Path.GetExtension(file.FileName);
                filenamewithoutext = Path.GetFileNameWithoutExtension(file.FileName);
                if (fileext != ".jpg" && fileext != ".png" && fileext != ".jpeg") continue;
                logoGuid = Guid.NewGuid().ToString();
                var coverfilename = logoGuid + fileext;
                var path = AppDomain.CurrentDomain.BaseDirectory + "Content\\Logo\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = Path.Combine(path, coverfilename);
                file.SaveAs(path);
                var thumbpath = AppDomain.CurrentDomain.BaseDirectory + "Content\\Logo\\";

                _filehelp.ConvertToThumbnail(file, logoGuid + "_th", fileext, thumbpath);
                fileName = file.FileName;


            }
            var res = Json(new { guid = logoGuid, filename = fileName, ext = fileext, filenamewithoutext = filenamewithoutext });

            return res;
        }
    }
}
