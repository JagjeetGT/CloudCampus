using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C5;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Service.Helpers;
using KRBAccounting.Web.CustomFilters;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.ViewModels.Library;
using KRBAccounting.Enums;
using KRBAccounting.Service;
using LinqKit;
using iTextSharp.text;

namespace KRBAccounting.Web.Controllers
{
    
    [Authorize]
    public class LibraryController : BaseController
    {
        private readonly IFormsAuthenticationService _authentication;
        private readonly IScBookRepository _scbookRepository;
        private readonly IScBookReceivedRepository _scbookreceivedRepository;
        private readonly IScBookReceivedDetailsRepository _scbookreceiveddetailsRepository;
        private readonly IScLibraryMemberRegistrationRepository _sclibrarymemberregistrationRepository;
        private readonly IScEmployeeCategoryRepository _scStaffGroupRepository;
        private readonly IScEmployeeInfoRepository _scStaffMasterRepository;
        private readonly IScStudentinfoRepository _scStudentinfoRepository;
        private readonly IScStudentRegistrationRepository _scStudentRegistrationRepository;
        private readonly IScStudentRegistrationDetailRepository _scStudentRegistrationDetailRepository;
        private readonly IScBookIssuedRepository _scbookissuedRepository;
        private readonly IScBookIssueReturnRepository _scbookissuereturnRepository;
        private readonly IScClassRepository _scClassRepository;
        private readonly ISystemControlRepository _systemControlRepository;
        private readonly IScLibrarySettingRepository _sclibrarysettingRepository;
        private readonly IScLibraryCardRepository _sclibrarycardRepository;
        private readonly IDocumentNumeringSchemeRepository _documentNumeringSchemeRepository;
        private readonly IScLibraryLateFineRepository _sclibrarylateFineRepository;
        private readonly IAccountTransactionRepository _accountTransactionRepository;
        private readonly ISystemControlRepository _systemControl;

        private int CurrentAcademyYearId;
        //
        // GET: /Library/
        public LibraryController(IScBookRepository scbookRepository,
                                 IScBookReceivedRepository scbookreceivedRepository,
                                 IScBookReceivedDetailsRepository scbookreceiveddetailsRepository,
                                 IScLibraryMemberRegistrationRepository sclibrarymemberregistrationRepository,
                                 IScEmployeeCategoryRepository scStaffGroupRepository,
                                 IScEmployeeInfoRepository scStaffMasterRepository,
                                 IScStudentinfoRepository scStudentinfoRepository,
                                 IScStudentRegistrationRepository studentRegistrationRepository,
                                 IScStudentRegistrationDetailRepository studentRegistrationDetailRepository,
                                 IScBookIssuedRepository scbookissuedRepository,
                                 IScBookIssueReturnRepository scbookissuereturnRepository,
                                 IScClassRepository scClassRepository,
            IScLibrarySettingRepository sclibrarysettingRepository,
            IScLibraryCardRepository sclibrarycardRepository,
            IDocumentNumeringSchemeRepository documentNumeringSchemeRepository,
        ISystemControlRepository systemControlRepository,
            IScLibraryLateFineRepository sclibrarylatefineRepository,
        IAccountTransactionRepository accountTransactionRepository,
            ISystemControlRepository systemControl)
        {
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.Library);
            _scbookRepository = scbookRepository;
            _scbookreceivedRepository = scbookreceivedRepository;
            _scbookreceiveddetailsRepository = scbookreceiveddetailsRepository;
            _sclibrarymemberregistrationRepository = sclibrarymemberregistrationRepository;
            _scStaffGroupRepository = scStaffGroupRepository;
            _scStaffMasterRepository = scStaffMasterRepository;
            _scStudentinfoRepository = scStudentinfoRepository;
            _scStudentRegistrationRepository = studentRegistrationRepository;
            _scStudentRegistrationDetailRepository = studentRegistrationDetailRepository;
            _scbookissuereturnRepository = scbookissuereturnRepository;
            _scClassRepository = scClassRepository;
            _scbookissuedRepository = scbookissuedRepository;
            _systemControlRepository = systemControlRepository;
            CurrentAcademyYearId = AcademicYear.Id;
            _sclibrarysettingRepository = sclibrarysettingRepository;
            _sclibrarycardRepository = sclibrarycardRepository;
            _documentNumeringSchemeRepository = documentNumeringSchemeRepository;
            _sclibrarylateFineRepository = sclibrarylatefineRepository;
            _accountTransactionRepository = accountTransactionRepository;
            _systemControl = systemControl;
        }
        public ActionResult DashBoard()
        {
            var viewModel = this.BuildLibraryDashBoardViewModel();
            var totalBooks = _scbookreceiveddetailsRepository.Count();
            if (totalBooks != 0)
            {
                viewModel.AvailableBooks = (_scbookreceiveddetailsRepository.Count(x => x.Status == 1)*100)/totalBooks;
                viewModel.NotAvailableBooks = (_scbookreceiveddetailsRepository.Count(x => x.Status == 2)*100)/
                                              totalBooks;
            }
            else

            {
                viewModel.AvailableBooks = 0;
                viewModel.NotAvailableBooks = 0;
            }
            return View(viewModel);
        }

        public LibraryDashBoardViewModel BuildLibraryDashBoardViewModel()
        {
           
            var viewModel = base.CreateViewModel<LibraryDashBoardViewModel>();
            viewModel.NewArrival =
                _scbookRepository.GetAll().OrderByDescending(x => x.Id);
            viewModel.DueBookIssueds = _scbookissuedRepository.GetMany(x => !x.IsReturn).OrderBy(x => x.Date);
            return viewModel;
        }
        #region Books
        [CheckPermission(Permissions = "Navigate", Module = "ScBos")]
        public ActionResult Books()
        {
            ViewBag.UserRight = base.UserRight("ScBos");
            return View();
        }
        [CheckPermission(Permissions = "Create", Module = "ScBos")]
        public ActionResult BookAdd()
        {
            var model = new ScBook();


            if (base.SystemControl.IsCurrDate)
            {
                model.DisplayYearPublication = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
            }
            model.SystemControl = _systemControlRepository.GetAll().FirstOrDefault();
          //model.YearOfPublication = DateTime.Now;
            //var sysCtrl = _systemControlRepository.GetAll().FirstOrDefault();
            //if (sysCtrl != null)
            //{
            //    model.SystemControl = sysCtrl;
            //}
            return View("_PartialBookAdd", model);
        }

        [HttpPost]
        public ActionResult BookAdd(ScBook model)
        {
            var user = _authentication.GetAuthenticatedUser();
            model.CreateById = user.Id;
            model.UpdateById = user.Id;
            model.CreatedDate = DateTime.UtcNow;
            model.UpdatedDate = DateTime.UtcNow;
         

            if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(model.DisplayYearPublication))
                {
                    model.YearOfPublication = Convert.ToDateTime(model.DisplayYearPublication);
                    model.MitiOfPublication = NepaliDateService.NepaliDate(Convert.ToDateTime(model.YearOfPublication)).Date;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(model.DisplayYearPublication))
                {
                    model.MitiOfPublication = model.DisplayYearPublication;
                    model.YearOfPublication = NepaliDateService.NepalitoEnglishDate(model.MitiOfPublication);
                }
                
            }
           
            if (ModelState.IsValid)
            {

                _scbookRepository.Add(model);
                //return Content("true");
                return RedirectToAction("Books");
            }
            var sysCtrl = _systemControlRepository.GetAll().FirstOrDefault();
            if (sysCtrl != null)
            {
                model.SystemControl = sysCtrl;
            }
            return View("_PartialBookAdd", model);
        }
        [CheckPermission(Permissions = "Edit", Module = "ScBos")]
        public ActionResult BookEdit(int id)
        {
            ScBook objScBook = _scbookRepository.GetById(x => x.Id == id);
            objScBook.SystemControl = _systemControlRepository.GetAll().FirstOrDefault();
            if (base.SystemControl.DateType == 1)
            {
                if (objScBook.YearOfPublication != null)
                {
                    objScBook.DisplayYearPublication = Convert.ToDateTime(objScBook.YearOfPublication).ToString("MM/dd/yyyy");

                }
              

            }
            else
            {
                if (!string.IsNullOrEmpty(objScBook.MitiOfPublication))
                {
                    objScBook.DisplayYearPublication = objScBook.MitiOfPublication;

                }
               
            }
            
            return View("_PartialBookEdit", objScBook);
        }

        [HttpPost]
        public ActionResult BookEdit(ScBook model)
        {
            var user = _authentication.GetAuthenticatedUser();
            model.UpdateById = user.Id;
            model.UpdatedDate = DateTime.UtcNow;


            if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(model.DisplayYearPublication))
                {
                    model.YearOfPublication = Convert.ToDateTime(model.DisplayYearPublication);
                    model.MitiOfPublication = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayYearPublication)).Date;
                }
            
            }
            else
            {
                if (!string.IsNullOrEmpty(model.DisplayYearPublication))
                {
                    model.MitiOfPublication = model.DisplayYearPublication;
                    model.YearOfPublication = NepaliDateService.NepalitoEnglishDate(model.MitiOfPublication);
                }
              
            }

            _scbookRepository.Update(model);
            return RedirectToAction("Books");
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScBos")]
        public ActionResult BooksList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScBos");
           
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            var lstBooks = _scbookRepository.GetPagedList(x => x.Id, pageNo, SystemControl.PageSize);
            return PartialView("_PartialBooksList",
                               lstBooks);
        }
         [CheckPermission(Permissions = "Delete", Module = "ScBos")]
        public ActionResult BookDelete(int id)
        {
            ScBook book = _scbookRepository.GetById(x => x.Id == id);
            var mapped = _scbookRepository.DeleteException(book);
            return Json(mapped, JsonRequestBehavior.AllowGet);
            // _scbookRepository.Delete(x => x.Id == id);
            //return RedirectToAction("Books");
        }


        //public JsonResult GetClassNo(string searchText)
        //{
        //    var list =
        //        _scbookRepository.Filter(x => x.ClassNumber.StartsWith(searchText)).OrderBy(x => x.ClassNumber).GroupBy(
        //            x => x.ClassNumber).ToList().Take(10);

        //    var data = from p in list select p.Key;
        //    // var data = list.GroupBy(x => x.City).ToList().Take(10);

        //    return Json(data.ToList());
        //}

        public ActionResult GetBooks()
        {
            var areas = _scbookRepository.GetAll().Select(x => new
                                                                   {
                                                                       Id = x.Id,
                                                                       Code = x.ShortName,
                                                                       Description = x.Name
                                                                   });
            return Json(areas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBookMFNNumberById(int id)
        {
            var data = _scbookRepository.GetById(id);
            return Json(new { number = data.MFNNumber }, JsonRequestBehavior.AllowGet);
        }

        #endregion Books

        #region BookReceiveds
        [CheckPermission(Permissions = "Navigate", Module = "ScBR")]
        public ActionResult BookReceived()
        {
            ViewBag.UserRight = base.UserRight("ScBR");
            var documentnumber = _documentNumeringSchemeRepository.GetById(x => x.ModuleName == "LBN");
            if (documentnumber == null)
            {
                ViewBag.ErrorMsg = "You havent added Document Numbering for Book Received yet. In order to add Book received, you need to add Document Numbering for Book Received  first.";
                ViewBag.Link = " <a href='/Setup/DocumentNumberingScheme' target='_blank'>Click Here To Add Document Numbering for Book Received</a>";
                return View("_Error");
            }
            return View();
        }
  [CheckPermission(Permissions = "Create", Module = "ScBR")]
        public ActionResult BookReceivedAdd()
        {
            var model = new ScBookReceived();
            model.Date = DateTime.UtcNow;
           
            if (base.SystemControl.IsCurrDate)
            {
                model.DisplayDate = base.SystemControl.DateType == 1 ? DateTime.Now.ToString("MM/dd/yyyy") : NepaliDateService.NepaliDate(DateTime.Now).Date;
              


            }
            model.SystemControl = _systemControlRepository.GetAll().FirstOrDefault();
            var doc = UtilityService.GetDocumentNumberingByModuleName("LBN");
            model.Number = doc;

           
          
            return PartialView("_PartialBookReceivedAdd", model);
        }

        [HttpPost]
        public ActionResult BookReceivedAdd(ScBookReceived model, IEnumerable<ScBookReceivedDetails> subjectEntry)
        {
            var usr = _authentication.GetAuthenticatedUser();

            model.CreatedDate = DateTime.UtcNow;
            model.CreatedById = usr.Id;
            model.UpdatedDate = DateTime.UtcNow;
            model.UpdatedById = usr.Id;

            if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(model.DisplayDate))
                {
                    model.Date = Convert.ToDateTime(model.DisplayDate);
                    model.Miti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayDate)).Date;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(model.DisplayDate))
                {
                    model.Miti = model.DisplayDate;
                    model.Date = NepaliDateService.NepalitoEnglishDate(model.Miti);
                }
            }
            _scbookreceivedRepository.Add(model);
            var documentnumber = _documentNumeringSchemeRepository.GetById(x => x.ModuleName == "LBN");

            documentnumber.DocCurrNo += 1;
            _documentNumeringSchemeRepository.Update(documentnumber);
            

            foreach (var data in subjectEntry.Where(x => x.AccessionNo != "").Select(item => new ScBookReceivedDetails()

                                                                                                 {

                                                                                                     AccessionNo = item.AccessionNo,
                                                                                                     Remarks = item.Remarks,
                                                                                                     MasterId = model.Id,
                                                                                                     BarCode = item.BarCode,
                                                                                                     Status = item.Status

                                                                                                 }))
            {
                _scbookreceiveddetailsRepository.Add(data);
            }
            return Content("True");
        }
          [CheckPermission(Permissions = "Edit", Module = "ScBR")]
        public ActionResult BookReceivedEdit(int bookreceivedId)
        {
            ScBookReceived objScBookReceived = _scbookreceivedRepository.GetById(x => x.Id == bookreceivedId);
            objScBookReceived.ReceivedDetailses =
              _scbookreceiveddetailsRepository.GetMany(x => x.MasterId == bookreceivedId);
            if (base.SystemControl.DateType == 1)
            {
                if (objScBookReceived.Date != null)
                {
                    objScBookReceived.DisplayDate = Convert.ToDateTime(objScBookReceived.Date).ToString("MM/dd/yyyy");

                }

            }
            else
            {
                if (!string.IsNullOrEmpty(objScBookReceived.Miti))
                {
                    objScBookReceived.DisplayDate = objScBookReceived.Miti;

                }

            }

            objScBookReceived.SystemControl = _systemControlRepository.GetAll().FirstOrDefault();
            return PartialView("_PartialBookReceivedEdit", objScBookReceived);
        }

        [HttpPost]
        public ActionResult BookReceivedEdit(ScBookReceived model, IEnumerable<ScBookReceivedDetails> subjectEntry)
        {
            var usr = _authentication.GetAuthenticatedUser();
            model.UpdatedDate = DateTime.UtcNow;
            model.UpdatedById = usr.Id;

            if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(model.DisplayDate))
                {
                    model.Date= Convert.ToDateTime(model.DisplayDate);
                    model.Miti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayDate)).Date;
                }
               
            }
            else
            {
                if (!string.IsNullOrEmpty(model.DisplayDate))
                {
                    model.Miti = model.DisplayDate;
                    model.Date = NepaliDateService.NepalitoEnglishDate(model.Miti);
                }
               
            }

            _scbookreceivedRepository.Update(model);
            _scbookreceiveddetailsRepository.Delete(x => x.MasterId == model.Id);

            foreach (var item in subjectEntry.Where(x => x.AccessionNo != null))
            {

                var data = new ScBookReceivedDetails()
                               {
                                   BarCode = item.BarCode,
                                   Status = item.Status,
                                   AccessionNo = item.AccessionNo,
                                   Remarks = item.Remarks,
                                   MasterId = model.Id

                               };

                _scbookreceiveddetailsRepository.Add(data);
            }
            return Content("True");
        }
          [CheckPermission(Permissions = "Navigate", Module = "ScBR")]
        public ActionResult BookReceivedList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScBR");
            var lstBookReceiveds = _scbookreceivedRepository.GetAll().OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialBookReceivedList",
                               lstBookReceiveds.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult BookReceivedDelete(int id)
        {
            var data = _scbookreceivedRepository.DeleteException(x => x.Id == id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BookReceivedDetails(int qtyCount)
        {
            var list = new List<ScBookReceivedDetails>();
            int i;
            for (i = 0; i < qtyCount; i++)
            {
                var data = new ScBookReceivedDetails();
                list.Add(data);
            }

            return PartialView("_PartialBookReceivedDetail", list);
        }

        #endregion

        #region LibraryMemberRegistrations

        public JsonResult GetStudentByTypeClassId(int id, string nextid)
        {
            var list =
                _sclibrarymemberregistrationRepository.GetMany(x => x.ClassId == id && x.Type == nextid).Select(
                    x => x.StudentId);
            var data =
                _scStudentinfoRepository.GetMany(x => !list.Contains(x.StudentID) && x.ClassId == id).Select(x => new
                                                                                                                      {
                                                                                                                          Id
                                                                                                                      =
                                                                                                                      x.
                                                                                                                      StudentID,
                                                                                                                          Description
                                                                                                                      =
                                                                                                                      x.
                                                                                                                      StuDesc,
                                                                                                                          Code
                                                                                                                      =
                                                                                                                      x.
                                                                                                                      StdCode,
                                                                                                                      });



            return Json(data, JsonRequestBehavior.AllowGet);
        }
             [CheckPermission(Permissions = "Navigate", Module = "ScLMR")]
        public ActionResult LibrarymemberRegistrations()
        {
            ViewBag.UserRight = base.UserRight("ScLMR");
            var docSLR = _documentNumeringSchemeRepository.GetById(x => x.ModuleName == "SLR");
            if (docSLR == null)
            {

                ViewBag.ErrorMsg = "You havent added document numbering for Student Library Registration. In order to add Book Issue, you need to add document numbering for Student Library Registration first.";
                ViewBag.Link = " <a href='/Setup/DocumentNumberingScheme' target='_blank'>Click Here To Add Document Numbering </a>";
                return View("_Error");
            }
            var docLCN = _documentNumeringSchemeRepository.GetById(x => x.ModuleName == "LCN");
            if (docLCN == null)
            {

                ViewBag.ErrorMsg = "You havent added document numbering for Library Card Number. In order to add Book Issue, you need to add document numbering for Library Card Number first.";
                ViewBag.Link = " <a href='/Setup/DocumentNumberingScheme' target='_blank'>Click Here To Add Document Numbering</a>";
                return View("_Error");
            }
            return View();
        }
          [CheckPermission(Permissions = "Create", Module = "ScLMR")]
        public ActionResult LibraryMemberRegistrationAdd()
        {

            List<SelectListItem> items = new List<SelectListItem>();
         
            items.Add(new SelectListItem
                          {
                              Text = "Student",
                              Value = "student",
                             
                          });
            items.Add(new SelectListItem
                          {
                              Text = "Staff",
                              Value = "staff"

                          });

            var model = new ScLibraryMemberRegistration { Typelist = items };
            return PartialView("_PartialLibraryMemberRegistrationAdd", model);
        }

        [HttpPost]
        public ActionResult LibraryMemberRegistrationAdd(ScLibraryMemberRegistration model,
                                                         IEnumerable<ScLibraryMemberRegistration> subjectEntry)
        {
            var usr = _authentication.GetAuthenticatedUser();
            var setting = _sclibrarysettingRepository.GetAll().FirstOrDefault();
            var regno = "0";
            var classId = 0;
            foreach (var item in subjectEntry.Where(x => x.StudentId != 0 || x.StaffId != 0))
            {
                regno = UtilityService.GetDocumentNumberingByModuleName("SLR");
                

                if (model.Type == "student")
                {
                    if (setting != null) 
                        if (setting.IsResgistrationNumberUse)
                        {
                            var stu = _scStudentinfoRepository.GetById(x => x.StudentID == item.StudentId);
                            if (stu != null)
                            {
                                regno = stu.Regno;

                            }

                        }
                        else
                        {
                            
                            if (string.IsNullOrEmpty(regno))
                            {
                                var stu = _scStudentinfoRepository.GetById(x => x.StudentID == item.StudentId);
                                if (stu != null)
                                {
                                    regno = stu.Regno;
                                }
                            }
                            else
                            {
                                base.UpdateDocumentNumbering("SLR");
                            }


                        }
                }
                else
                {
                    base.UpdateDocumentNumbering("SLR");
                }

                var data = new ScLibraryMemberRegistration()
                {
                    Type = model.Type,
                    ClassId = UtilityService.GetClassByStudentId(item.StudentId, CurrentAcademyYearId),
                    StudentId = item.StudentId,
                    StaffId = item.StaffId,
                    RegNo = regno,
                    CreateById = usr.Id,
                    UpdateById = usr.Id,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    AcademyYearId = CurrentAcademyYearId
                };
                _sclibrarymemberregistrationRepository.Add(data);

                if (setting == null) continue;
                for (var i = 1; i <= setting.TotalCardIssue; i++)
                {
                    var cardno = UtilityService.GetDocumentNumberingByModuleName("LCN");

                    var card = new ScLibraryCard()
                                   {
                                       CardNo = cardno,
                                       LibraryRegistrationId = data.Id,
                                       IsUse = false
                                   };
                    _sclibrarycardRepository.Add(card);

                    base.UpdateDocumentNumbering("LCN");
                }
            }


            return Content("True");
        }

        public ActionResult GetLibraryRegistrationClasses()
        {
            var data = _sclibrarymemberregistrationRepository.GetAll().Select(x => x.ClassId);

            List<SchClass> lstClasses = _scClassRepository.GetMany(x => !data.Contains(x.Id)).OrderBy(x => x.Schedule).ToList();

            var classes = lstClasses.Select(x => new
            {
                Id = x.Id,
                Code = x.Code,
                Description = x.Description
            });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult LibraryMemberRegistrationEdit(int librarymemberregistrationId)
        //{
        //    List<SelectListItem> items = new List<SelectListItem>();
        //    items.Add(new SelectListItem
        //                  {
        //                      Text = "Student",
        //                      Value = "student",
        //                      Selected = true
        //                  });
        //    items.Add(new SelectListItem
        //                  {
        //                      Text = "Staff",
        //                      Value = "staff"

        //                  });

        //    var data = _sclibrarymemberregistrationRepository.GetById(x => x.Id == librarymemberregistrationId);
        //    data.OldType = data.Type;
        //    data.OLdClassId = data.ClassId;
        //    data.MemberRegistrations =
        //        _sclibrarymemberregistrationRepository.GetMany(x => x.ClassId == data.ClassId && x.Type == data.Type);
        //    data.Typelist = items;
        //    return PartialView("_PartialLibraryMemberRegistrationEdit", data);
        //}

        //[HttpPost]
        //public ActionResult LibraryMemberRegistrationEdit(ScLibraryMemberRegistration model,
        //                                                  IEnumerable<ScLibraryMemberRegistration> subjectEntry)
        //{
        //    var usr = _authentication.GetAuthenticatedUser();
        //    var setting = _sclibrarysettingRepository.GetAll().FirstOrDefault();
        //    if (setting != null)
        //        if (setting.IsResgistrationNumberUse)
        //        {

        //        }

        //    _sclibrarymemberregistrationRepository.Delete(x => x.ClassId == model.OLdClassId && x.Type == model.OldType);
        //    foreach (var item in subjectEntry.Where(x => x.StudentId != 0 || x.StaffId != 0))
        //    {
        //        var data = new ScLibraryMemberRegistration()
        //                       {
        //                           Type = model.Type,
        //                           ClassId = model.ClassId,
        //                           StudentId = item.StudentId,
        //                           StaffId = item.StaffId,
        //                           RegNo = item.RegNo,
        //                           //Card1 = item.Card1,
        //                           //Card2 = item.Card2,
        //                           //Card3 = item.Card3,
        //                           CreateById = model.CreateById,
        //                           UpdateById = usr.Id,
        //                           CreatedDate = model.CreatedDate,
        //                           UpdatedDate = DateTime.UtcNow,
        //                           Status = item.Status
        //                       };
        //        _sclibrarymemberregistrationRepository.Add(data);

        //        if (setting != null)
        //        {
        //            for (int i = 1; i <= setting.TotalCardIssue; i++)
        //            {
        //                var card = new ScLibraryCard()
        //                               {
        //                                   CardNo = "asdf",
        //                                   LibraryRegistrationId = data.Id
        //                               };
        //                _sclibrarycardRepository.Add(card);
        //            }
        //        }
        //    }
        //    return Content("True");
        //}

        public ActionResult LibraryMemberRegistrationsDetailList(string type, int classId, int pageNo = 1)
        {

          var data =  _sclibrarymemberregistrationRepository.GetPagedList(
                x => x.AcademyYearId == CurrentAcademyYearId && x.Type == type && x.ClassId == classId, j => j.RegNo,
                pageNo , this.SystemControl.PageSize);
            //=
            //    _sclibrarymemberregistrationRepository.GetMany(
            //        x => x.AcademyYearId == CurrentAcademyYearId && x.Type == type && x.ClassId == classId);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialLibraryMemberRegistrationsDetailList",
                               data);
        }
        public ActionResult LibraryMemberRegistrationDetailAdd(string type)
        {
            var list = new List<ScLibraryMemberRegistration>();
            if (type == "student")
            {
                var libRegisteredStudent =
                    _sclibrarymemberregistrationRepository.GetMany(x => x.Type.Trim() == type.Trim()).Select(x => x.StudentId);
                var student =
                    _scStudentRegistrationDetailRepository.GetMany(
                        x =>
                        !libRegisteredStudent.Contains(x.StudentId) &&
                        x.StudentRegistration.AcademicYearId == CurrentAcademyYearId).GroupBy(
                            x => x.StudentId);

                list.AddRange(from item in student
                              where item.FirstOrDefault() != null
                              select new ScLibraryMemberRegistration()
                                         {
                                             RollNo = item.FirstOrDefault().RollNo,
                                             StudentId = item.FirstOrDefault().StudentId,
                                             Studentinfo = item.FirstOrDefault().Studentinfo,
                                             Type = type,
                                             Section = item.FirstOrDefault().Section == null ? string.Empty : item.FirstOrDefault().Section.Description
                                         });
            }
            else
            {
                var libRegisteredStudent =
                   _sclibrarymemberregistrationRepository.GetMany(x => x.Type.Trim() == type.Trim()).Select(x => x.StaffId);
                var staff = _scStaffMasterRepository.GetMany(x => !libRegisteredStudent.Contains(x.Id));

                list.AddRange(staff.Select(item => new ScLibraryMemberRegistration()
                                                       {
                                                           Type = type,
                                                           StaffId = item.Id,
                                                           StaffMaster = item,
                                                       }));
            }


            var partialView = this.RenderPartialViewToString("_PartialLibraryMemberRegistrationDetail", list);


            return Json(new { view = partialView, value = list.Count() }, JsonRequestBehavior.AllowGet);


        }
          [CheckPermission(Permissions = "Navigate", Module = "ScLMR")]
        public ActionResult LibraryMemberRegistrationsList(int pageNo = 1)
        {
            var data = from d in _sclibrarymemberregistrationRepository.GetAll()
                       group d by new { d.Type, d.ClassId }
                           into g
                           select g.ToList();
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;

            return PartialView("_PartialLibraryMemberRegistrationsList",
                               data.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }
         [CheckPermission(Permissions = "Delete", Module = "ScLMR")]
        public ActionResult LibraryMemberRegistrationDelete(int id)
        {

            _sclibrarymemberregistrationRepository.Delete(x => x.Id == id);

            return RedirectToAction("LibraryMemberRegistrations");
        }



        #endregion

        #region BookIssueds
        public ActionResult BookIssueDetail(string accNo)
        {
            var err = "";
            var view = "";
            var bookrec =
                _scbookreceiveddetailsRepository.GetById(x => x.AccessionNo.Trim().ToLower() == accNo.Trim().ToLower());
            if (bookrec != null)
            {
                if (bookrec.BookReceived.Book.IsIssuable)
                {
                    if (bookrec.Status == 1)
                    {
                        var setting = _sclibrarysettingRepository.GetAll().FirstOrDefault();
                        var data = new LibraryBookIssueViewModel()
                                       {
                                           BookIssued = new ScBookIssued()
                                                            {
                                                                BookDueDate =
                                                                    DateTime.Now.Date.AddDays(setting.BookDueDate),
                                                                Date = DateTime.Now.Date,
                                                            },
                                           BookReceivedDetails = bookrec,

                                       };

                        view = RenderPartialViewToString("_PartialIssueBook", data);

                    }
                    else
                    {
                        err = "Book has already been " + Enum.GetName(typeof (LibraryBookStatus), bookrec.Status);
                    }
                }
                else
                {
                    err = "Sorry Book cannot be Issue";
                }
            }
            else
            {
                err = "Book not found with number " + accNo;

            }
            return Json(new { msgerr = err, partialView = view }, JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScBI")]
        public ActionResult BookIssued()
        {
            ViewBag.UserRight = base.UserRight("ScBI");
            return View();
        }
         [CheckPermission(Permissions = "Create", Module = "ScBI")]
        public ActionResult BookIssuedAdd()
         {
            


            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem
            {
                Text = "Student",
                Value = "student",
                Selected = true
            });
            items.Add(new SelectListItem
            {
                Text = "Staff",
                Value = "staff"

            });
            var data = new LibraryBookIssueViewModel()
            {
                ScLibraryCards = _sclibrarycardRepository.GetMany(x => x.LibraryMemberRegistration.AcademyYearId == CurrentAcademyYearId)

            };

            return View("_PartialBookIssuedAdd", data);
        }

        [HttpPost]
        public ActionResult BookIssuedAdd(LibraryBookIssueViewModel model)
        {
            var err = "";
            var success = "";
            try
            {
var miti = NepaliDateService.NepaliDate(model.BookIssued.Date);
            var duemiti = NepaliDateService.NepaliDate(model.BookIssued.BookDueDate);
            var usr = _authentication.GetAuthenticatedUser();
            var bookissue = new ScBookIssued()
                                {
                                    BookDueDate = model.BookIssued.BookDueDate,
                                    BookDueMiti = duemiti.Date,
                                    CardId = model.CardId,
                                    Date = model.BookIssued.Date,
                                    Miti = miti.Date,
                                    BookReceivedDetailId = model.BookReceivedDetails.Id,
                                    CreatedDate = DateTime.Now,
                                    CreatedById = usr.Id,
                                    AcademyYearId = CurrentAcademyYearId,
                                    IsReturn = false
                                };
            _scbookissuedRepository.Add(bookissue);
            var bookdetail = _scbookreceiveddetailsRepository.GetById(x => x.Id == model.BookReceivedDetails.Id);
            bookdetail.Status = (int)LibraryBookStatus.Borrowed;
            _scbookreceiveddetailsRepository.Update(bookdetail);

           var card = _sclibrarycardRepository.GetById(x => x.Id == model.CardId);
            card.IsUse = true;
            _sclibrarycardRepository.Update(card);


                success = "Book Have been Issued";
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }

            return Json(new {msgerr = err, success = success}, JsonRequestBehavior.AllowGet);
        }
         [CheckPermission(Permissions = "Edit", Module = "ScBI")]
        public ActionResult BookIssuedEdit(int bookissuedId)
        {



            ScBookIssued objScBookIssued = _scbookissuedRepository.GetById(x => x.Id == bookissuedId);



            return View("_PartialBookIssuedEdit", objScBookIssued);
        }

        [HttpPost]
        public ActionResult BookIssuedEdit(ScBookIssued model)
        {
            ScBookIssued objScBookIssued = _scbookissuedRepository.GetById(x => x.Id == model.Id);

            _scbookissuedRepository.Update(objScBookIssued);
            //return RedirectToAction("BookIssuedEdit", new { bookissuedId = objScBookIssued.Id });


            return Content("True");
        }
         [CheckPermission(Permissions = "Navigate", Module = "ScBI")]
        public ActionResult BookIssuedList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScBI");
            var lstBookIssueds = _scbookissuedRepository.GetMany(x=>x.AcademyYearId == CurrentAcademyYearId).OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialBookIssuedList",
                               lstBookIssueds.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult BookIssuedDelete(int bookissuedId)
        {
            _scbookissuedRepository.Delete(x => x.Id == bookissuedId);
            return RedirectToAction("BookIssued");
        }

        public JsonResult GetBook()
        {

            List<ScBook> lstBook = _scbookRepository.GetAll().OrderBy(x => x.BookNumber).ToList();

            var classes = lstBook.Select(x => new
                                                  {
                                                      Id = x.Id,
                                                      Number = x.BookNumber,
                                                      Description = x.Name,
                                                      Publication = x.Publisher,

                                                  });
            return Json(classes, JsonRequestBehavior.AllowGet);
        }

        #endregion BookIssueds

        #region BookIssueReturns
        [CheckPermission(Permissions = "Navigate", Module = "ScBIR")]
        public ActionResult BookIssueReturns()
        {
            ViewBag.UserRight = UserRight("ScBIR");
            return View();
        }
         [CheckPermission(Permissions = "Create", Module = "ScBIR")]
        public ActionResult BookIssueReturnAdd()
        {
           
            var docLLF = _documentNumeringSchemeRepository.GetById(x => x.ModuleName == "LLF");
            if (docLLF == null)
            {

                ViewBag.ErrorMsg = "You havent added document numbering for  Library Late Fine. In order to add Book Issue Return, you need to add document numbering for Library Late Fine first.";
                ViewBag.Link = " <a href='/Setup/DocumentNumberingScheme' target='_blank'>Click Here To Add Document Numbering </a>";
                return View("_Error");
            }
            if (!_sclibrarylateFineRepository.GetAll().Any())
            {
                //hasError = true;
                ViewBag.ErrorMsg = "You havent added any late fine yet. In order to add Book Issue Return, you need to add late fine first.";
                ViewBag.Link = " <a href='/Library/LibraryLateFine' target='_blank'>Click Here To Add Late fine</a>";
                return View("_Error");
            }
          

           
            var model = base.CreateViewModel<LibraryBookIssueReturnDetialViewModel>();
             return View("_PartialBookIssueReturnAdd", model);
        }

        [HttpPost]
        public ActionResult BookIssueReturnAdd(ScBookIssued model)
        {
           var err = "";
            var success = "";
            try
            {
                var miti = NepaliDateService.NepaliDate(DateTime.Now.Date);
           
            var usr = _authentication.GetAuthenticatedUser();
            var bookissue = new ScBookIssueReturn()
                                {
                                    
                                    CardId = model.CardId,
                                    ReturnDate = DateTime.Now.Date,
                                    ReturnMiti = miti.Date,
                                    BookDetailId = model.BookReceivedDetails.Id,
                                    CreatedById = usr.Id,
                                    FineAmount = model.FineAmount,
                                    IssueId = model.Id,
                                    AcademyYearId = CurrentAcademyYearId
                                };
                
                
                

            _scbookissuereturnRepository.Add(bookissue);
            var bookdetail = _scbookreceiveddetailsRepository.GetById(x => x.Id == model.BookReceivedDetails.Id);
            bookdetail.Status = (int)LibraryBookStatus.Available;
            _scbookreceiveddetailsRepository.Update(bookdetail);
                var dcoumentNumbering = _documentNumeringSchemeRepository.GetById(x => x.ModuleName == "LLF");
                var Source = StringEnum.Parse(typeof(ModuleEnum), "Library Late Fine").ToString();
               
           var card = _sclibrarycardRepository.GetById(x => x.Id == model.CardId);
            card.IsUse = false;
            _sclibrarycardRepository.Update(card);
            var issue = _scbookissuedRepository.GetById(x => x.CardId == model.CardId && !x.IsReturn);
            issue.IsReturn = true;
            _scbookissuedRepository.Update(issue);
                if (model.FineAmount != 0)
                {
                    var accountTransactionDetail = new AccountTransaction();
                    accountTransactionDetail.VNo = UtilityService.GetDocumentNumberingByModuleName(Source);
                    accountTransactionDetail.VDate = DateTime.UtcNow;
                    accountTransactionDetail.GlCode = base.SystemControl.CashBook;
                    accountTransactionDetail.SlCode = 0;
                    accountTransactionDetail.ReferenceId = model.Id;
                    accountTransactionDetail.DrAmt = Convert.ToDecimal(model.FineAmount);
                    accountTransactionDetail.LocalDrAmt = Convert.ToDecimal(model.FineAmount);
                    accountTransactionDetail.Narration = "LL fine";
                    accountTransactionDetail.Source = Source;
                    accountTransactionDetail.SNo = 1;
                    accountTransactionDetail.CreatedById = usr.Id;
                    accountTransactionDetail.FYId = FiscalYear.Id;
                    accountTransactionDetail.BranchId = base.CurrentBranch.Id;
                    _accountTransactionRepository.Add(accountTransactionDetail);

                    var accountTransactionMaster = new AccountTransaction();
                    accountTransactionMaster.VNo = UtilityService.GetDocumentNumberingByModuleName(Source);
                    accountTransactionMaster.VDate = DateTime.UtcNow;
                    accountTransactionMaster.GlCode = base.SystemControl.LibraryLateFine;
                    accountTransactionMaster.SlCode = 0;
                    accountTransactionMaster.ReferenceId = model.Id;
                    accountTransactionMaster.CrAmt = Convert.ToDecimal(model.FineAmount);
                    accountTransactionMaster.LocalCrAmt = Convert.ToDecimal(model.FineAmount);
                    accountTransactionMaster.Narration ="LL fine";
                    accountTransactionMaster.Source = Source;
                    accountTransactionMaster.SNo = 1;
                    accountTransactionMaster.CreatedById = usr.Id;
                    accountTransactionMaster.FYId = FiscalYear.Id;
                    accountTransactionMaster.BranchId = base.CurrentBranch.Id;
                    _accountTransactionRepository.Add(accountTransactionMaster);

                
                }
                

                success = "Book Have been Return";
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }

            return Json(new {msgerr = err, success = success}, JsonRequestBehavior.AllowGet);
        }




         [CheckPermission(Permissions = "Navigate", Module = "ScBR")]
        public ActionResult BookIssueReturnsList(int pageNo = 1)
        {
            var lstBookIssueReturns = _scbookissuereturnRepository.GetMany(x=>x.AcademyYearId == CurrentAcademyYearId).OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialBookIssueReturnsList", lstBookIssueReturns.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult BookIssueReturnDelete(int bookissuereturnId)
        {
            ScBookIssueReturn objScBookIssueReturn = _scbookissuereturnRepository.GetById(x => x.Id == bookissuereturnId);
            _scbookissuereturnRepository.Delete(objScBookIssueReturn);
            return RedirectToAction("BookIssueReturns");
        }

        public ActionResult BookIssueReturnDetail(string accNo)
        {
            ScLibraryLateFine fine;
            decimal fineamount = 0;
            var err = "";
            var view = "";
            var bookdetail =
                _scbookreceiveddetailsRepository.GetById(x => x.AccessionNo.ToLower().Trim() == accNo.ToLower().Trim());
            if (bookdetail != null)
            {
                if (bookdetail.Status ==(int)LibraryBookStatus.Borrowed)
                {
                    var currentday = DateTime.Now;


                    var booissue = _scbookissuedRepository.GetMany(x => x.BookReceivedDetailId == bookdetail.Id && x.AcademyYearId ==CurrentAcademyYearId).LastOrDefault();
                    var dueday = booissue.BookDueDate;
                   var day = currentday - dueday;
                    var diffday = day.TotalDays;
                    var max = _sclibrarylateFineRepository.GetAll().Max(x => x.DayStart);
                    if(diffday >= max)
                    {
                         fine = _sclibrarylateFineRepository.GetById(x =>x.DayStart==max);

                    }
                    else
                    {
                         fine = _sclibrarylateFineRepository.GetById(x =>diffday >= x.DayStart && diffday <= x.DayEnd);
                    }


                    if (fine != null)
                    {
                         fineamount = fine.Amount;
                    }
                    view = RenderPartialViewToString("_PartialBookReturn", booissue);

                }
                else
                {
                    err = "Book has not been Issue Yet";
                }
                
            }
            else
            {
                err = "Book not found with number " + accNo;

            }
            return Json(new { msgerr = err, partialView = view ,fineAmount=fineamount}, JsonRequestBehavior.AllowGet);
           
            
        }
        #endregion BookIssueReturns
        #region Library Setting
         [CheckPermission(Permissions = "Navigate", Module = "SCLMS")]
        public ActionResult LibrarySetting()
        {
            var data = _sclibrarysettingRepository.GetAll().FirstOrDefault() ?? new ScLibrarySetting();


            return View(data);
        }
        [HttpPost]
        public ActionResult LibrarySetting(ScLibrarySetting model)
        {


            if (model.Id == 0)
            {
                _sclibrarysettingRepository.Add(model);
            }
            else
            {
                _sclibrarysettingRepository.Update(model);
            }
            return RedirectToAction("LibrarySetting");
        }




        #endregion

        #region Library LateFee
        public JsonResult LibraryLateFineTitleExists(string title, int Id)
        {
            var Title = new ScLibraryLateFine();
            if (Id != 0)
            {
                var data = _sclibrarylateFineRepository.GetById(x => x.Id == Id);
                if (data.Title.ToLower().Trim() != title.ToLower().Trim())
                {
                    Title = _sclibrarylateFineRepository.GetById(x => x.Title.ToLower().Trim() == title.ToLower().Trim());

                }
            }
            else
            {
                Title = _sclibrarylateFineRepository.GetById(x => x.Title.ToLower().Trim() == title.ToLower().Trim());
            }
            if (Title == null)
            {
                Title = new ScLibraryLateFine();
            }

            return Title.Id == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json("Already Exists", JsonRequestBehavior.AllowGet);
        }
         [CheckPermission(Permissions = "Navigate", Module = "LibLF")]
        public ActionResult LibraryLateFine()
        {
            ViewBag.UserRight = base.UserRight("LibLF");
            return View();
        }
         [CheckPermission(Permissions = "Create", Module = "LibLF")]
        public ActionResult LibraryLateFineAdd()
        {
            var model = new ScLibraryLateFine();
            return PartialView("_PartialLibraryLateFineAdd", model);
        }
        [HttpPost]
        public ActionResult LibraryLateFineAdd(ScLibraryLateFine model)
        {
            if (ModelState.IsValid)
            {
            var user = _authentication.GetAuthenticatedUser();
            model.CreatedBy = user.Id;
            model.ModifiedBy = user.Id;
            model.CreatedOn = DateTime.UtcNow;
            model.ModifiedOn = DateTime.UtcNow;
            model.IsDeleted = false;

           
                _sclibrarylateFineRepository.Add(model);
                return Content("True");
                
            }


            return Content("Error Occured");



        }
         [CheckPermission(Permissions = "Edit", Module = "LibLF")]
        public ActionResult LibraryLateFineEdit(int id)
        {
            ScLibraryLateFine objScFine = _sclibrarylateFineRepository.GetById(x => x.Id == id);

            return PartialView("_PartialLibraryLateFineEdit", objScFine);
        }

        [HttpPost]
        public ActionResult LibraryLateFineEdit(ScLibraryLateFine model)
        {
            if (ModelState.IsValid)
            {
                var user = _authentication.GetAuthenticatedUser();
                model.ModifiedBy = user.Id;
                model.ModifiedOn = DateTime.UtcNow;
                _sclibrarylateFineRepository.Update(model);
                return Content("True");
            }
            return Content("Error Occured");
        }
        [CheckPermission(Permissions = "Navigate", Module = "LibLF")]
        public ActionResult LibraryLateFineList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("LibLF");
            var LateFine = _sclibrarylateFineRepository.GetMany(x=>x.IsDeleted==false).OrderByDescending(x => x.Id);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("_PartialLibraryLateFineList",
                               LateFine.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public ActionResult LibraryLateFineDelete(int id)
        {
           
            ScLibraryLateFine fine = _sclibrarylateFineRepository.GetById(x => x.Id == id);
            var data = _sclibrarylateFineRepository.DeleteException(fine);
            return Json(data, JsonRequestBehavior.AllowGet);
       
        }

        #endregion

       // public ActionResult GetLibraryRegistrationDetailByCardNo(string cardNo, string type)
             public ActionResult GetLibraryRegistrationDetailByCardNo(string cardNo)
        {
            var err = "";
            var view = "";
            var cardId = 0;
            var name = "";
            var prefix = "";
            var suffix = "";
            var result = "";

            var documentstring = _documentNumeringSchemeRepository.GetById(x => x.ModuleName == "LCN");
             prefix = documentstring.DocPrefix;
            suffix = documentstring.DocSuffix;
            result = prefix + cardNo + suffix; 
        
            var data =
                _sclibrarycardRepository.GetById(
                    x =>
                    x.CardNo.ToLower().Trim() == result.ToLower().Trim() &&
                    x.LibraryMemberRegistration.AcademyYearId == CurrentAcademyYearId 
                    );
            if(data !=null)
            {
               
                if(data.IsUse)
                {
                    err =  "This Card Is Already Used";
                }
                if(data.LibraryMemberRegistration.Type=="student")
                {
                    name = data.LibraryMemberRegistration.Studentinfo.StuDesc;
                }
                if (data.LibraryMemberRegistration.Type == "staff")
                {
                    name = data.LibraryMemberRegistration.StaffMaster.Description;
                }
                else
                {
                 view = RenderPartialViewToString("_PartialMemberDetail",data.LibraryMemberRegistration);
                    cardId = data.Id;
                    //if (type.ToLower().Trim() == "student") name = data.LibraryMemberRegistration.Studentinfo.StuDesc;
                    //else name = data.LibraryMemberRegistration.StaffMaster.Description;
                }
            }
            else
            {
                err = cardNo + " Not Found  ";
            }
            return Json(new { msgerr = err, partialView = view, name = name, cardno = result, cardid = cardId },
                        JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Navigate", Module = "SCLBS")]
        public ActionResult BookSearch()
        {
            return View();
        }
       [HttpPost]
        public ActionResult BookSearch(string book)
       {
           var view = "";
           var  err = "";
          var bookrec =  _scbookreceiveddetailsRepository.GetMany(
               x =>
               x.AccessionNo.Contains(book.Trim()) || x.BookReceived.Book.Name.Contains(book.Trim()) ||
               x.BookReceived.Book.Keywords.Contains(book.Trim()) || x.BookReceived.Book.PersonalAuthor.Contains(book.Trim()) || x.BarCode == book);
           if(bookrec.Any())
           {
               view = RenderPartialViewToString("_PartialBookSearchList", bookrec);
           }
           else
           {
               err = "Books not found with your search query. Try again. ";
           }
           return Json(new { msgerr = err, partialView = view },
                        JsonRequestBehavior.AllowGet);
       }
       public ActionResult BookSearchByName(string name)
       {
           var err = "";
           var view = "";
          var card = _sclibrarycardRepository.GetById(x => x.CardNo == name);

           var cardId = 0;
           if(card !=null)
           {
               cardId = card.Id;
           }
           var predicate = PredicateBuilder.True<ScBookIssued>();
           predicate = predicate.And(x => x.LibraryCard.LibraryMemberRegistration.AcademyYearId == CurrentAcademyYearId);
          
           predicate = predicate.And(x => x.CardId == cardId);
           //predicate=predicate.And(x => x.LibraryCard.LibraryMemberRegistration.Type.ToLower().Trim() == type.ToLower().Trim());
         // predicate = type.ToLower().Trim() =="student" ? predicate.And(x => x.LibraryCard.LibraryMemberRegistration.Studentinfo.StuDesc.Contains(name)) : predicate.And(x => x.LibraryCard.LibraryMemberRegistration.StaffMaster.Description.Contains(name));
           var data =
               _scbookissuedRepository.GetExpandable(predicate);
           if (data != null)
           {
               view = RenderPartialViewToString("_PartialBookSearchByNameList", data);
           }
           else
           {
               err = "Books not found with your search query. Try again. ";
           }
        
           return Json(new { msgerr = err, partialView = view },
                        JsonRequestBehavior.AllowGet);
       }
    }
}
