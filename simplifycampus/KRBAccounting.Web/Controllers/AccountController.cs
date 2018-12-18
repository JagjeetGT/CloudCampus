using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Security;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Enums;
using KRBAccounting.Service;
using KRBAccounting.Service.Helpers;
using KRBAccounting.Web.CustomFilters;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.ViewModels;
using KRBAccounting.Web.Helpers;
namespace KRBAccounting.Web.Controllers
{
   // [CheckFirstInstallation]
    //[CheckModulePermissionAttribute("")]
    public class AccountController : Controller
    {
        private readonly IUserRepository _user;
        private readonly IRoleRepository _role;
        private readonly ISecurityService _securityService;
        private readonly FileHelper _filehelp = new FileHelper();
        private readonly IFormsAuthenticationService _authentication;
        private readonly ISystemControlRepository _systemControl;
        private readonly ICompanyInfoRepository _company;
        public AccountController(ICompanyInfoRepository companyInfoRepository, IUserRepository userRepository, IRoleRepository roleRepository, ISecurityService securityService, ISystemControlRepository systemControlRepository)
        {
            _company = companyInfoRepository;
            _role = roleRepository;
            _user = userRepository;
            _securityService = securityService;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.Setting);
            _systemControl = systemControlRepository;
        }

        #region Setting

        public ActionResult Setting()
        {

            var user = _authentication.GetAuthenticatedUser();
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("LogOn");
        }
        [HttpPost]
        public ActionResult UserInfo(User model)
        {
            if (ModelState.IsValid)
            {
                var user = _securityService.GetUserById(model.Id);
                if (user.Username == model.Username || _securityService.IsUserNameAvailable(model.Username))
                {
                    user.FullName = model.FullName;

                    user.Username = model.Username;

                    user.EmailAddress = model.EmailAddress;
                    user.ImageGuid = model.ImageGuid;
                    user.Ext = model.Ext;
                    _securityService.UpdateUser(user);
                    _authentication.SignOut();
                    _authentication.SignIn(user, false);
                    return Content("true");

                }
                return Content("User name already exists. Please enter a different user name.");

            }
            return RedirectToAction("Setting");
        }
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    var ranchId = _authentication.GetAuthenticatedUser().LastAccessedBranch;
                    var currentuser = _securityService.CheckLogin(User.Identity.Name, model.OldPassword, ranchId);
                    if (currentuser == null)
                    {
                        return Content("Invalid Password");

                    }

                    currentuser.Password = PasswordHelper.HashPassword(model.NewPassword);
                    changePasswordSucceeded = _securityService.ChangePasswrod(currentuser);

                    //MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    //changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return Content("true");
                }
                else
                {
                    return Content("The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [HttpPost]
        public ActionResult UploadImages(IEnumerable<HttpPostedFileBase> attachments)
        {
            var imgGuid = string.Empty;
            var fileName = string.Empty;
            var fileext = string.Empty;
            var filenamewithoutext = string.Empty;
            var newGUid = string.Empty;
            foreach (var file in attachments)
            {
                fileext = Path.GetExtension(file.FileName);
                filenamewithoutext = Path.GetFileNameWithoutExtension(file.FileName);
                if (fileext != ".jpg" && fileext != ".png" && fileext != ".jpeg") continue;
                imgGuid = Guid.NewGuid().ToString();
                var coverfilename = imgGuid + fileext;
                var path = AppDomain.CurrentDomain.BaseDirectory + "Content\\UserImages\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path = Path.Combine(path, coverfilename);
                file.SaveAs(path);
                var thumbpath = AppDomain.CurrentDomain.BaseDirectory + "Content\\UserImages\\";

                _filehelp.ConvertToThumbnail(file, imgGuid + "_th", fileext, thumbpath);
                fileName = file.FileName;
                //imgGuid = imgGuid + fileext;

            }
            var res = Json(new { guid = imgGuid, filename = fileName, ext = fileext, filenamewithoutext = filenamewithoutext });

            return res;
        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logon()
        {
            var logonViewModel = new LogOnModel();
            logonViewModel.EnableBranch = _systemControl.GetAll().FirstOrDefault().EnableBranch;
            if (!logonViewModel.EnableBranch)
                logonViewModel.BranchId = _company.Filter(x => x.ParentId == 0).FirstOrDefault().Id;
            else
                logonViewModel.BranchList = new SelectList(_company.GetAll(), "Id", "Name");
            return View("Logon", logonViewModel);
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _securityService.CheckLogin(model.UserName, model.Password, model.BranchId);
                if (user != null)
                {

                    var mode1l = _user.GetById(x => x.Id == user.Id);
                    mode1l.LastAccessedBranch = model.BranchId;
                    mode1l.LastLoginDate = DateTime.UtcNow;
                    _user.Update(mode1l);
                    _authentication.SignIn(user, model.RememberMe);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        var usr = user.Roles;

                        var student = usr.FirstOrDefault(x => x.RoleName.ToLower() == "student");
                        if (student != null)
                        {
                            return RedirectToAction("Index", "StudentProfile");
                        }
                        var teacher = usr.FirstOrDefault(x => x.RoleName.ToLower() == "teacher");
                        if (teacher != null)
                        {
                            return RedirectToAction("Index", "Teacher");
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            model.EnableBranch = _systemControl.GetAll().FirstOrDefault().EnableBranch;
            if (!model.EnableBranch)
                model.BranchId = _company.Filter(x => x.ParentId == 0).FirstOrDefault().Id;
            else
                model.BranchList = new SelectList(_company.Filter(x => x.ParentId != 0), "Id", "Name");
            return View(model);
        }


        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            foreach (DictionaryEntry dCache in HttpContext.Cache)
            {
                HttpContext.Cache.Remove(dCache.Key.ToString());
            }
            _authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
