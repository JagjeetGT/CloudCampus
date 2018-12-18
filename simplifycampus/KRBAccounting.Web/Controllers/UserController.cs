using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using KRBAccounting.Data;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Enums;
using KRBAccounting.Service;
using KRBAccounting.Service.Helpers;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Web.CustomFilters;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.Helpers;
using KRBAccounting.Web.ViewModels;

namespace KRBAccounting.Web.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {

        private readonly IUserRepository _user;
        private readonly IRoleRepository _role;
        private readonly ISecurityService _securityService;
        private readonly IModuleListRepository _moduleList;
        private readonly ISecurityRightRepository _securityRight;
        private readonly ICompanyInfoRepository _companyInfo;
        private readonly FileHelper _filehelp = new FileHelper();
        private readonly IFormsAuthenticationService _authentication;
        private readonly IMenuItemRepository _menuitemRepository;
        public UserController(IUserRepository userRepository,
            IRoleRepository roleRepository,
            ISecurityService securityService,
            IModuleListRepository moduleListRepository,
            ISecurityRightRepository securityRightRepository,
            ICompanyInfoRepository companyInfoRepository,
                IMenuItemRepository menuitemRepository)
        {
            _role = roleRepository;
            _user = userRepository;
            _securityService = securityService;
            _moduleList = moduleListRepository;
            _securityRight = securityRightRepository;
            _companyInfo = companyInfoRepository;
            _menuitemRepository = menuitemRepository;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.Setting);
        }
        //
        // GET: /User/

        #region User
        [CheckPermission(Permissions = "Navigate", Module = "UM")]
        public ActionResult Users()
        {
            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "UM")]
        public ActionResult UserList()
        {
            ViewBag.UserRight = base.UserRight("UM");
            var data = _securityService.GetUsersByCompanyId(this.CompanyInfo.Id);
            return PartialView(data);
        }
        [CheckPermission(Permissions = "Create", Module = "UM")]
        public ActionResult UserAdd()
        {
            ViewBag.RolesList = _securityService.Roles();
            var user = new User { IsActive = true };
            var companyId = base.CompanyInfo.Id;
            ViewBag.EnableBranch = SystemControl.EnableBranch;
            ViewBag.BranchList = new SelectList(_companyInfo.GetAll(), "Id", "Name");
            return PartialView(user);

        }

        [HttpPost]
        public ActionResult UserAdd(User model, List<int> role)
        {
            if (ModelState.IsValid)
            {
                if (_securityService.IsUserNameAvailable(model.Username))
                {
                    model.IsDeleted = false;
                    model.LastLoginDate = DateTime.UtcNow;
                    model.LastLoginIp = string.Empty;
                    model.Password = PasswordHelper.HashPassword(model.Password);
                    model.UpdatedDate = DateTime.UtcNow;
                    model.CreatedDate = DateTime.UtcNow;
                    model.CompanyId = this.CompanyInfo.Id;
                    model.BranchId = !SystemControl.EnableBranch ? this.CompanyInfo.Id : model.BranchId;
                    model.ImageGuid = model.ImageGuid;
                    model.Ext = model.Ext;
                    model.IsActive = true;
                    _user.Add(model);
                    if (role == null)
                        role = new List<int>();
                    _securityService.AssignRole(model.Id, role);

                    return RedirectToAction("Users");
                }
                ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");
            }
            //var roleslist = new List<Role>();
            //if (role != null)
            //{
            //    roleslist.AddRange(role.Select(r => _securityService.GetRole(r)).Where(data => data != null));
            //}
            //model.Roles = roleslist;
            //ViewBag.RolesList = _securityService.Roles();
            //return PartialView(model);
            return RedirectToAction("Users");
        }
        public ActionResult UserDelete(int id)
        {
            var user = _securityService.GetUserById(id);
            user.IsDeleted = true;
            _securityService.UpdateUser(user);
            return RedirectToAction("Users");
        }
        [CheckPermission(Permissions = "Edit", Module = "UM")]
        public ActionResult UserEdit(int id)
        {
            var data = _securityService.GetUserById(id);
            ViewBag.RolesList = _securityService.Roles();
            var companyId = base.CompanyInfo.Id;
            ViewBag.EnableBranch = SystemControl.EnableBranch;
            ViewBag.BranchList = new SelectList(_companyInfo.GetAll(), "Id", "Name");
            return PartialView(data);
        }
        [HttpPost]
        public ActionResult UserEdit(User model, List<int> role)
        {
            var roleslist = new List<Role>();

            if (ModelState.IsValid)
            {
                var user = _securityService.GetUserById(model.Id);
                if (user.Username == model.Username || _securityService.IsUserNameAvailable(model.Username))
                {
                    user.FullName = model.FullName;

                    user.Username = model.Username;
                    user.IsActive = model.IsActive;
                    user.EmailAddress = model.EmailAddress;
                    user.ImageGuid = model.ImageGuid;
                    user.Ext = model.Ext;
                    user.CompanyId = base.CompanyInfo.Id;
                    user.BranchId = !SystemControl.EnableBranch ? this.CompanyInfo.Id : model.BranchId;
                    _securityService.UpdateUser(user);
                    if (role == null)
                        role = new List<int>();
                    _securityService.AssignRole(user.Id, role);
                    return null;

                }

                if (role != null)
                {
                    roleslist.AddRange(role.Select(r => _securityService.GetRole(r)).Where(data => data != null));
                }
                model.Roles = roleslist;
                ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");

            }
            ViewBag.RolesList = _securityService.Roles();
            return PartialView(model);
        }
        [CheckPermission(Permissions = "Edit", Module = "UM")]
        public ActionResult ChangePassword(int id)
        {
            var model = new ChangePasswordAdmin();
            int userId = _user.GetById(id).Id;
            model.UserId = userId;

            return PartialView("_PartialChangePassword", model);

            //var userId = _user.GetById(id);
            //return PartialView("_PartialChangePassword", userId);
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordAdmin model)
        {
            var user = _securityService.GetUserById(model.UserId);
            if (user != null)
            {
                if (model.NewPassword.Length > 3)
                {
                    user.Password = PasswordHelper.HashPassword(model.NewPassword);
                    _securityService.UpdateUser(user);
                }
            }
            return null;
        }


        [HttpPost]
        public ActionResult UploadImages(IEnumerable<HttpPostedFileBase> attachments)
        {
            var imgGuid = string.Empty;
            var fileName = string.Empty;
            var fileext = string.Empty;
            var filenamewithoutext = string.Empty;
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


            }
            var res = Json(new { guid = imgGuid, filename = fileName, ext = fileext, filenamewithoutext = filenamewithoutext });

            return res;
        }
        #endregion

        #region Role Master
        [CheckPermission(Permissions = "Navigate", Module = "UM")]
        public ActionResult Roles()
        {

            return View();
        }
        [CheckPermission(Permissions = "Edit", Module = "UM")]
        public ActionResult RoleList()
        {
            var data = _securityService.Roles();
            return PartialView(data);
        }
        [CheckPermission(Permissions = "Create", Module = "UM")]
        public ActionResult RoleAdd()
        {
            var role = new Role();
            return PartialView(role);
        }
        [HttpPost]
        public ActionResult RoleAdd(Role model)
        {
            _securityService.CreateRole(model);
            var modules = _moduleList.GetAll();
            foreach (var item in modules)
            {
                var securityRight = new SecurityRight();
                securityRight.ModuleId = item.Id;
                securityRight.Role = model.Id;
                securityRight.Create = true;
                securityRight.Delete = false;
                securityRight.Edit = false;
                securityRight.Navigate = true;
                securityRight.Approve = false;
                _securityRight.Add(securityRight);
            }
            return RedirectToAction("SecurityRight", new { role = model.Id });
        }
        public JsonResult CheckRoleName(string RoleName, int Id)
        {
            var role = new Role();
            if (Id != 0)
            {
                var data = _role.GetById(x => x.Id == Id);
                if (data.RoleName.ToLower().Trim() != RoleName.ToLower().Trim())
                {
                    role =
                        _role.GetById(x => x.RoleName.ToLower().Trim() == RoleName.Trim().ToLower());

                }
            }
            else
            {
                role = _role.GetById(x => x.RoleName.ToLower().Trim() == RoleName.Trim().ToLower());
            }
            if (role == null)
            {
                role = new Role();
            }

            return role.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Edit", Module = "UM")]
        public ActionResult RoleEdit(int role)
        {
            var d = _securityRight.GetMany(x => x.Role == role).Select(x => x.ModuleId);
            var modules = _moduleList.GetMany(x => !d.Contains(x.Id));
            foreach (var item in modules)
            {
                var securityRight = new SecurityRight();
                securityRight.ModuleId = item.Id;
                securityRight.Role = role;
                securityRight.Create = true;
                securityRight.Delete = false;
                securityRight.Edit = false;
                securityRight.Navigate = true;
                securityRight.Approve = false;
                _securityRight.Add(securityRight);
            }

            var data = _securityService.GetRole(role);
            return PartialView(data);
        }
        [HttpPost]
        public ActionResult RoleEdit(Role model)
        {
            _securityService.EditRole(model);
            return null;
        }

        #endregion

        #region Security Rights
        [CheckPermission(Permissions = "Create", Module = "SRG")]
        public ActionResult SecurityRight(int role)
        {
            var dataContext = new DataContext();
            //  var modules = _moduleList.GetAll();
            ViewBag.Role = role;

            var modules = _menuitemRepository.GetAll();

            var securityRights = (from n in _securityRight.GetMany(x => x.Role == role)
                                  join ml in dataContext.ModuleLists on n.ModuleId equals ml.Id
                                  join m in _menuitemRepository.GetAll() on ml.Id equals m.ModuleId
                                      into j
                                  from jt in j.DefaultIfEmpty()
                                 // orderby n.Id
                                  select new SecurityRight()
                                             {
                                                 Id = n.Id,
                                                 Delete = n.Delete,
                                                 Approve = n.Approve,
                                                 Create = n.Create,
                                                 Edit = n.Edit,
                                                 ModuleId = n.ModuleId,
                                                 Navigate = n.Navigate,
                                                 ParentId = jt==null? 0:jt.ParentId,
                                                 Role = n.Role,
                                                 MenuItemId = jt == null ? 0 : jt.Id,
                                                 LinkName = jt == null ? ml.Name : jt.LinkText,
                                                 Schedule = jt==null? 0:jt.Schedule

                                             }

                                 ).OrderBy(x=>x.Schedule).ToList();
            if (!securityRights.Any())
            {
                securityRights = new List<SecurityRight>();
            }



            return View(securityRights);
        }

        [HttpPost]
        public ActionResult SecurityRight(ICollection<SecurityRight> securityRight)
        {
            foreach (var item in securityRight)
            {

                var context = new DataContext();
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
            }
            return RedirectToAction("SecurityRight", new { role = securityRight.FirstOrDefault().Role });
        }
        #endregion
    }
}
