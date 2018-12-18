using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using KRBAccounting.Data;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Web.CustomFilters;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.Helpers;
using KRBAccounting.Data.Infrastructure;
using KRBAccounting.Web.Models;
using KRBAccounting.Web.ViewModels.Academy;
using KRBAccounting.Web.ViewModels.Entry;
using KRBAccounting.Web.ViewModels.School;
using KRBAccounting.Enums;
using KRBAccounting.Service;
using KRBAccounting.Service.Helpers;
using KRBAccounting.Domain.StoredProcedures;
using KRBAccounting.Web.ViewModels.Sms;
using RestSharp;
using Sex = KRBAccounting.Web.Helpers.Sex;
using System.Web.UI.WebControls;
using LinqKit;



namespace KRBAccounting.Web.Controllers
{
    [Authorize]
   
    public class SmsController : BaseController
    {
        private readonly IScClassRepository _classRepository;
        private readonly IScEmployeeInfoRepository _scStaffMasterRepository;
        private readonly ISmsGroupRepository _smsgroupRepository;
        private readonly ISmsGroupDetailRepository _smsgroupdetailRepository;
        private readonly IFormsAuthenticationService _authentication;
        public readonly IScStudentRegistrationDetailRepository _scStudentRegistrationDetailRepository;
        private readonly IScStudentinfoRepository _scStudentinfoRepository;
        private readonly ISmsLogRepository _smslogRepository;
        private readonly ISmsTemplatesRepository _smstemplatesRepository;
        private readonly ISMSSettingsRepository _smssettingsRepository;
        private int CurrentAcademyYearId;

        #region Constructor

        public SmsController(IScClassRepository classRepository, ISmsTemplatesRepository smstemplatesRepository, ISMSSettingsRepository smssettingsRepository, IScStudentinfoRepository scStudentinfoRepository, IScStudentRegistrationDetailRepository scStudentRegistrationDetailRepository, IScEmployeeInfoRepository scStaffMasterRepository, ISmsGroupRepository smsgroupRepository, ISmsGroupDetailRepository smsgroupdetailRepository, ISmsLogRepository smslogRepository)
        {
            _classRepository = classRepository;
            _scStaffMasterRepository = scStaffMasterRepository;
            _smsgroupRepository = smsgroupRepository;
            _smsgroupdetailRepository = smsgroupdetailRepository;
            _scStudentRegistrationDetailRepository = scStudentRegistrationDetailRepository;
            CurrentAcademyYearId = AcademicYear.Id;
            _smslogRepository = smslogRepository;
            _smstemplatesRepository = smstemplatesRepository;
            _scStudentinfoRepository = scStudentinfoRepository;
            _smssettingsRepository = smssettingsRepository;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.SMS);
        }
        #endregion
        public ActionResult DashBoard()
        {
            return RedirectToAction("SendSms");
           // return View();
        }
        #region SmsGroup

        #region Groups

        public JsonResult GetStaffMasters()
        {
            var staffs = _scStaffMasterRepository.GetAll();

            var classes = staffs.Select(x => new
            {
                Id = x.Id,
                Code = x.Code,
                Description = x.Description
            });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStudentByClassId(int id)
        {

            var data = _scStudentRegistrationDetailRepository.GetMany(x => x.StudentRegistration.ClassId == id && x.StudentRegistration.AcademicYearId == CurrentAcademyYearId).Select(x => new
            {
                Id = x.StudentId,

                Description = x.Studentinfo.StuDesc,
                Code = x.Studentinfo.StdCode,
            });



            return Json(data, JsonRequestBehavior.AllowGet);
        }
         [CheckPermission(Permissions = "Navigate", Module = "SMSGrp")]
        public ActionResult Groups()
        {
            ViewBag.UserRight = base.UserRight("SMSGrp");
            return View();
        }
         [CheckPermission(Permissions = "Create", Module = "SMSGrp")]
        public ActionResult GroupAdd()
        {
            var model = new SmsGroup();
            model.GroupList = new SelectList(
                    Enum.GetValues(typeof(GroupType)).Cast<GroupType>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            model.Classlist = new SelectList(_classRepository.GetAll(), "Id", "Description");
            model.Stafflist = new SelectList(_scStaffMasterRepository.GetAll(), "Id", "Description");
            return View("_PartialGroupAdd", model);
        }

        [HttpPost]
        public ActionResult GroupAdd(SmsGroup model, string[] multiselect)
        {
            if (ModelState.IsValid)
            {
                model.CreatedBy = _authentication.GetAuthenticatedUser().Id;
                model.CreatedOn = DateTime.UtcNow;
                model.ModifiedBy = _authentication.GetAuthenticatedUser().Id;
                model.ModifiedOn = DateTime.UtcNow;
                _smsgroupRepository.Add(model);
                foreach (var i in multiselect)
                {
                    var arrVal = i.Split('_');
                    var id = arrVal[0];
                    var type = arrVal[1];
                    var detail = new SmsGroupDetail();
                    detail.GroupId = model.Id;
                    detail.ReferenceId = Convert.ToInt32(id);
                    if (type.ToLower() == "staff")
                    {
                        detail.ReferenceType = (int)GroupType.Staff;
                    }
                    else
                    {
                        detail.ReferenceType = (int)GroupType.Student;
                    }

                    _smsgroupdetailRepository.Add(detail);
                }
                return Content("true");
            }
            return Content("false");
        }
 [CheckPermission(Permissions = "Edit", Module = "SMSGrp")]
        public ActionResult GroupEdit(int groupId)
        {
            SmsGroup objSmsGroup = _smsgroupRepository.GetById(x => x.Id == groupId);
            objSmsGroup.GroupList = new SelectList(
                    Enum.GetValues(typeof(GroupType)).Cast<GroupType>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            objSmsGroup.Classlist = new SelectList(_classRepository.GetAll(), "Id", "Description");
            objSmsGroup.SmsGroupDetailList = _smsgroupdetailRepository.GetMany(x => x.GroupId == groupId);
            var detailIdlist = _smsgroupdetailRepository.GetMany(x => x.GroupId == groupId).Select(x => x.ReferenceId);
            objSmsGroup.Stafflist = new SelectList(_scStaffMasterRepository.GetMany(x => !detailIdlist.Contains(x.Id)), "Id", "Description");


            return View("_PartialGroupEdit", objSmsGroup);
        }

        [HttpPost]
        public ActionResult GroupEdit(SmsGroup model, string[] multiselect)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedOn = DateTime.UtcNow;
                model.ModifiedBy = _authentication.GetAuthenticatedUser().Id;
                _smsgroupRepository.Update(model);

                _smsgroupdetailRepository.Delete(x => x.GroupId == model.Id);
                foreach (var i in multiselect)
                {
                    var arrVal = i.Split('_');
                    var id = arrVal[0];
                    var type = arrVal[1];
                    var detail = new SmsGroupDetail();
                    detail.GroupId = model.Id;
                    detail.ReferenceId = Convert.ToInt32(id);
                    if (type.ToLower() == "staff")
                    {
                        detail.ReferenceType = (int)GroupType.Staff;
                    }
                    else
                    {
                        detail.ReferenceType = (int)GroupType.Student;
                    }

                    _smsgroupdetailRepository.Add(detail);
                }
                return Content("true");
            }
            return Content("false");

        }
         [CheckPermission(Permissions = "Navigate", Module = "SMSGrp")]
        public ActionResult GroupsList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("SMSGrp");

            var lstGroups = _smsgroupRepository.GetPagedList(x => x.Id, pageNo, SystemControl.PageSize);
            return PartialView("_PartialListGroups", lstGroups);
        }

        public ActionResult GroupDelete(int groupId)
        {
            SmsGroup objSmsGroup = _smsgroupRepository.GetById(x => x.Id == groupId);
            _smsgroupRepository.Delete(objSmsGroup);
            return RedirectToAction("Groups");
        }

        public JsonResult CheckSmsGroup(string Name, int Id)
        {

            var group = new SmsGroup();
            if (Id != 0)
            {
                var data = _smsgroupRepository.GetById(x => x.Id == Id);
                if (data.Name.ToLower().Trim() != Name.ToLower().Trim())
                {
                    group =
                        _smsgroupRepository.GetById(x => x.Name.ToLower().Trim() == Name.Trim().ToLower());

                }
            }
            else
            {
                group = _smsgroupRepository.GetById(x => x.Name.ToLower().Trim() == Name.Trim().ToLower());
            }
            if (group == null)
            {
                group = new SmsGroup();
            }

            return group.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }


        #endregion Groups

        #region SendGroupSms
        [CheckPermission(Permissions = "Navigate", Module = "SMSGS")]
        public ActionResult SendSmsGroup(string success)
        {
            var model = new ScSmsGroupViewModel();
            model.TemplateList = new SelectList(_smstemplatesRepository.GetAll(), "Id", "Name");
            if (success!=null)
            {
                ViewBag.Success = "true";
            }
            return View(model);

        }

        [HttpPost]
        public ActionResult SendSmsGroup(ScSmsGroupViewModel model)
        {
            var groupdetails = _smsgroupdetailRepository.GetMany(x => x.GroupId == model.SmsGroupId);
            var destination = string.Empty;
            var i = 0;
            foreach (var item in groupdetails)
            {

                if (i > 0)
                {
                    destination += ",";
                }
                var number = String.Empty;
                if (item.ReferenceType == (int)GroupType.Staff)
                {
                    number = _scStaffMasterRepository.GetById(item.ReferenceId).Mobile;
                }
                else
                {
                    number = _scStudentinfoRepository.Get(x => x.StudentID == item.ReferenceId).FMobile;
                }
                if (number != null)
                {
                    destination += number;
                }
                i++;
            }

            var msg = Url.Encode(model.SmsText);
            var smssettings = _smssettingsRepository.GetAll();
            var username = String.Empty;
            var password = string.Empty;
            if (smssettings.Any())
            {
                username = smssettings.FirstOrDefault().UserName;
                password = CryptorEngine.Decrypt(smssettings.FirstOrDefault().Password, true);
            }

            if (username != "" && password != "")
            {
                string url =
               "http://vas.easy.com.np/api/?username=" + username + "&password=" + password + "&type=0&dlr=1&destination=" + destination +
               "&source=KRBAcademy&message=" + msg;
                RestSharp.RestClient client = new RestClient(url);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);

                var request = new RestRequest();
                //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
                //request.AddUrlSegment("id", 123); // replaces matching token in request.Resource
                // request.AddUrlSegment("method", "POST");
                // easily add HTTP Headers
                client.AddDefaultHeader("Accept", "application/*+xml;version=1.5");

                // execute the request
                var response = client.Execute(request);
                var content = response.Content;
                if (content.Trim().Contains("1701:SUCCESS!"))
                {
                    foreach (var item in groupdetails)
                    {
                        var smslog = new SmsLog();
                        smslog.ReferenceId = item.ReferenceId;
                        smslog.ReferenceType = item.ReferenceType;
                        if (item.ReferenceType == (int)GroupType.Staff)
                        {
                            smslog.SentToNumber = _scStaffMasterRepository.GetById(item.ReferenceId).Mobile;
                        }
                        else
                        {
                            smslog.SentToNumber = _scStudentinfoRepository.GetById(item.ReferenceId).FMobile;

                        }
                        smslog.SentFromId = _authentication.GetAuthenticatedUser().Id;
                        smslog.Body = model.SmsText;
                        smslog.IsSent = true;
                        smslog.SendDate = DateTime.UtcNow;

                        _smslogRepository.Add(smslog);
                    }

                    return Content("True");

                }
                return Content("False");
            }


            return Content("NoUsername");
        }
        [CheckPermission(Permissions = "Navigate", Module = "SMSGS")]
        public ActionResult Getgrouplist()
        {
            var busList = _smsgroupRepository.GetAll().Select(x => new
            {
                Id = x.Id,
                Description = x.Name
            });
            return Json(busList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTemplate(int id)
        {
            var templateBody = string.Empty;
            var template = _smstemplatesRepository.GetById(id);
            if (template != null)
            {
                templateBody = template.Body;
            }
            return Json(templateBody, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region SendSms
        [CheckPermission(Permissions = "Navigate", Module = "SMSS")]
        public ActionResult SendSms(string success)
        {
            var model = new ScSmsSendViewModel();
            model.SmsGroup = new SmsGroup();
            model.SmsGroup.GroupList = new SelectList(
                   Enum.GetValues(typeof(GroupType)).Cast<GroupType>().Select(
                       x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            model.SmsGroup.Classlist = new SelectList(_classRepository.GetAll(), "Id", "Description");
            model.SmsGroup.Stafflist = new SelectList(_scStaffMasterRepository.GetAll(), "Id", "Description");
            if(success!=null)
            {
                ViewBag.Success = "true";
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult SendSms(ScSmsSendViewModel model, string[] multiselect)
        {
            var destination = string.Empty;
            var count = 0;
            if (multiselect != null && multiselect.Count() != 0)
            {
                foreach (var i in multiselect)
                {
                    if (count > 0)
                    {
                        destination += ",";
                    }

                    var arrVal = i.Split('_');
                    var id = arrVal[0];
                    var type = arrVal[1];
                    var Id = Convert.ToInt32(id);

                    if (type.ToLower() == "staff")
                    {
                        destination += _scStaffMasterRepository.GetById(Id).Mobile;
                    }
                    else
                    {
                        destination += _scStudentinfoRepository.Get(x => x.StudentID == Id).FMobile;
                    }
                    count++;
                }
            }
            if (model.MobileNumbers != null)
            {
                destination += model.MobileNumbers;
            }

            var msg = Url.Encode(model.SmsText);

            var smssettings = _smssettingsRepository.GetAll();
            var username = String.Empty;
            var password = string.Empty;
            if (smssettings.Any())
            {
                username = smssettings.FirstOrDefault().UserName;
                password = CryptorEngine.Decrypt(smssettings.FirstOrDefault().Password, true);
            }

            if (username != "" && password != "")
            {

                string url =
                    "http://vas.easy.com.np/api/?username=" + username + "&password=" + password + "&type=0&dlr=1&destination=" +
                    destination +
                    "&source=KRBAcademy&message=" + msg;
                RestSharp.RestClient client = new RestClient(url);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);

                var request = new RestRequest();
                //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
                //request.AddUrlSegment("id", 123); // replaces matching token in request.Resource
                // request.AddUrlSegment("method", "POST");
                // easily add HTTP Headers
                client.AddDefaultHeader("Accept", "application/*+xml;version=1.5");

                // execute the request
                var response = client.Execute(request);
                var content = response.Content;
                if (content.Trim().Contains("1701:SUCCESS!"))
                {
                    foreach (var item in multiselect)
                    {
                        var arrVal = item.Split('_');
                        var referenceid = arrVal[0];
                        var type = arrVal[1];
                        var Id = Convert.ToInt32(referenceid);

                        var smslog = new SmsLog();
                        smslog.ReferenceId = Id;
                        if (type.ToLower() == "staff")
                        {
                            smslog.ReferenceType = (int)GroupType.Staff;
                            smslog.SentToNumber = _scStaffMasterRepository.GetById(Id).Mobile;
                        }
                        else
                        {
                            smslog.ReferenceType = (int)GroupType.Student;
                            smslog.SentToNumber = _scStudentinfoRepository.Get(x => x.StudentID == Id).FMobile;

                        }
                        smslog.SentFromId = _authentication.GetAuthenticatedUser().Id;
                        smslog.Body = model.SmsText;
                        smslog.IsSent = true;
                        smslog.SendDate = DateTime.UtcNow;

                        _smslogRepository.Add(smslog);
                    }

                    if (model.MobileNumbers != null)
                    {
                        var numberArray = model.MobileNumbers.Split(',');
                        foreach (var item in numberArray)
                        {
                            var smslog = new SmsLog();
                            smslog.ReferenceId = 0;
                            smslog.SentToNumber = item;
                            smslog.SentFromId = _authentication.GetAuthenticatedUser().Id;
                            smslog.Body = model.SmsText;
                            smslog.IsSent = true;
                            smslog.SendDate = DateTime.UtcNow;

                            _smslogRepository.Add(smslog);
                        }
                    }


                    return Content("True");

                }
                return Content("False");
            }
            return Content("NoUsername");
        }


        #endregion


        #region Templatess
         [CheckPermission(Permissions = "Navigate", Module = "SMSTML")]
        public ActionResult Templatess()
        {
            ViewBag.UserRight = base.UserRight("SMSTML");

            return View();
        }
         [CheckPermission(Permissions = "Create", Module = "SMSTML")]
        public ActionResult TemplatesAdd()
        {
            var model = new SmsTemplates();
            return PartialView("_PartialTemplatesAdd", model);
        }

        [HttpPost]
        public ActionResult TemplatesAdd(SmsTemplates model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedBy = _authentication.GetAuthenticatedUser().Id;
                model.CreatedOn = DateTime.UtcNow;
                model.ModifiedOn = DateTime.UtcNow;
                model.ModifiedBy = _authentication.GetAuthenticatedUser().Id;
                _smstemplatesRepository.Add(model);
                return Content("True");
            }
            return Content("False");

        }
         [CheckPermission(Permissions = "Edit", Module = "SMSTML")]
        public ActionResult TemplatesEdit(int templatesId)
        {
            SmsTemplates objSmsTemplates = _smstemplatesRepository.GetById(x => x.Id == templatesId);
            return PartialView("_PartialTemplatesEdit", objSmsTemplates);
        }

        [HttpPost]
        public ActionResult TemplatesEdit(SmsTemplates model)
        {
            SmsTemplates objSmsTemplates = _smstemplatesRepository.GetById(x => x.Id == model.Id);
            objSmsTemplates.Name = model.Name;
            objSmsTemplates.Body = model.Body;
            objSmsTemplates.CreatedOn = model.CreatedOn;
            objSmsTemplates.CreatedBy = model.CreatedBy;
            objSmsTemplates.ModifiedOn = DateTime.UtcNow;
            objSmsTemplates.ModifiedBy = _authentication.GetAuthenticatedUser().Id;
            _smstemplatesRepository.Update(objSmsTemplates);
            return Content("True");
        }
         [CheckPermission(Permissions = "Navigate", Module = "SMSTML")]
        public ActionResult TemplatessList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("SMSTML");
            var lstTemplatess = _smstemplatesRepository.GetAll().OrderByDescending(x => x.Id);
            return PartialView("_PartialListTemplatess", lstTemplatess.AsQueryable().ToPagedList(pageNo, SystemControl.PageSize));
        }
         [CheckPermission(Permissions = "Delete", Module = "SMSTML")]
        public ActionResult TemplatesDelete(int templatesId)
        {
            SmsTemplates objSmsTemplates = _smstemplatesRepository.GetById(x => x.Id == templatesId);
            _smstemplatesRepository.Delete(objSmsTemplates);
            return Content("True");
        }

        public JsonResult CheckTemplates(string Name, int Id)
        {

            var group = new SmsTemplates();
            if (Id != 0)
            {
                var data = _smstemplatesRepository.GetById(x => x.Id == Id);
                if (data.Name.ToLower().Trim() != Name.ToLower().Trim())
                {
                    group =
                        _smstemplatesRepository.GetById(x => x.Name.ToLower().Trim() == Name.Trim().ToLower());

                }
            }
            else
            {
                group = _smstemplatesRepository.GetById(x => x.Name.ToLower().Trim() == Name.Trim().ToLower());
            }
            if (group == null)
            {
                group = new SmsTemplates();
            }

            return group.Id == 0
                       ? Json(true, JsonRequestBehavior.AllowGet)
                       : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
        #endregion Templatess

        #region SMSSettingss
         [CheckPermission(Permissions = "Navigate", Module = "SMSStg")]
        public ActionResult SMSSettings()
        {
            var model = new SMSSettings();
            var smssettings = _smssettingsRepository.GetAll();
            if(smssettings.Any())
            {
                model = smssettings.FirstOrDefault();
                model.Password = CryptorEngine.Decrypt(model.Password, true);
            }
            return View("_PartialSMSSettingsAdd", model);
        }

        [HttpPost]
        public ActionResult SMSSettings(SMSSettings model)
        {
            if (ModelState.IsValid)
            {
                if(model.Id!=0)
                {
                    model.Password = CryptorEngine.Encrypt(model.Password, true);
                    _smssettingsRepository.Update(model);
                }
                else
                {
                    model.Password = CryptorEngine.Encrypt(model.Password, true);
                    _smssettingsRepository.Add(model);
                }
               
                return Content("True");
            }

            return Content("False");

        }
        #endregion SMSSettingss

        #endregion
    }
}

