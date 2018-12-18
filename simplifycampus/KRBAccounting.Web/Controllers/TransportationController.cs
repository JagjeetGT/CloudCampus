using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Web.CustomFilters;
using KRBAccounting.Web.CustomProviders;
using KRBAccounting.Web.ViewModels.Academy;
using KRBAccounting.Enums;
using KRBAccounting.Service;
using LinqKit;

namespace KRBAccounting.Web.Controllers
{
    [CheckModulePermission("Academy")]
    public class TransportationController : BaseController
    {

        private readonly ISystemControlRepository _systemControlRepository;

        private readonly IScStudentinfoRepository _scStudentinfoRepository;

        private readonly IScBusRepository _scBusRepository;
        private readonly IScBusStopRepository _scBusStopRepository;
        private readonly IScBusRouteDetailsRepository _scBusRouteDetailsRepository;

        private readonly IScTransportMappingRepository _scTransportMappingRepository;
        private readonly IFormsAuthenticationService _authentication;
        public readonly IScStudentRegistrationRepository _scStudentRegistrationRepository;
        public readonly IScStudentRegistrationDetailRepository _scStudentRegistrationDetailRepository;
        public readonly IStudentExtraActivityRepository _StudentExtraActivityRepository;
   

        private int CurrentAcademyYearId;
        #region Constructor

        public TransportationController(

                                IScStudentinfoRepository scStudentinfoRepository,

          

                                IScBusRepository scBusRepository,

                                IScBusStopRepository scBusStopRepository,

                                IScBusRouteDetailsRepository scBusRouteDetailsRepository,

                                ISystemControlRepository systemControlRepository,

                                IScTransportMappingRepository scTransportMappingRepository,

                                IScStudentRegistrationRepository studentRegistrationRepository,
                                IScStudentRegistrationDetailRepository StudentRegistrationDetail,
                                IStudentExtraActivityRepository studentExtraActivityRepository
            


            )
        {

            _scStudentinfoRepository = scStudentinfoRepository;

            _scBusRepository = scBusRepository;

            _scBusStopRepository = scBusStopRepository;

            _scBusRouteDetailsRepository = scBusRouteDetailsRepository;

            _systemControlRepository = systemControlRepository;

            _scTransportMappingRepository = scTransportMappingRepository;

            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
            _authentication.SetCategoryId((int)HeaderMenu.Academy);
            _scStudentRegistrationRepository = studentRegistrationRepository;
            _scStudentRegistrationDetailRepository = StudentRegistrationDetail;
            _StudentExtraActivityRepository = studentExtraActivityRepository;

            CurrentAcademyYearId = AcademicYear.Id;

        }

        #endregion



        public ActionResult Transportation()
        {
            return RedirectToAction("Bus");
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScB")]
        public ActionResult Bus()
        {
            ViewBag.UserRight = base.UserRight("ScB");
            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScB")]
        public ActionResult BusList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScB");
           
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;

            var data = _scBusRepository.GetPagedList(x => x.Id, pageNo, this.SystemControl.PageSize);
            return PartialView("ScBusList", data);
        }
        [CheckPermission(Permissions = "Create", Module = "ScB")]
        public ActionResult BusAdd()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult BusAdd(ScBus model)
        {
            ScBus bus = _scBusRepository.Filter(x => x.Description.Equals(model.Description)).FirstOrDefault();
            if (bus == null)
            {
                _scBusRepository.Add(model);
                return Content("True");
            }
            else
            {
                return Content("Description already exists.");
            }

        }
        [CheckPermission(Permissions = "Edit", Module = "ScB")]
        public ActionResult BusEdit(int busId)
        {
            var editBus = _scBusRepository.GetById(x => x.Id == busId);
            return PartialView(editBus);
        }

        [HttpPost]
        public ActionResult BusEdit(ScBus model)
        {
            var busToEdit = _scBusRepository.GetById(x => x.Id == model.Id);
            ScBus bus = _scBusRepository.Filter(x => x.Description.Equals(model.Description) && x.Description != busToEdit.Description).FirstOrDefault();
            if (bus == null)
            {
                var _dataContext = new DataContext();
                _dataContext.Entry(model).State = EntityState.Modified;
                _dataContext.SaveChanges();
                return Content("True");
            }
            else
            {
                return Content("Description already exist.");
            }


        }
          [CheckPermission(Permissions = "Delete", Module = "ScB")]
        public ActionResult DeleteBus(int busId)
        {
            var data = _scBusRepository.DeleteException(x => x.Id == busId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // Bus-Stop Actions from here
        [CheckPermission(Permissions = "Navigate", Module = "ScBS")]
        public ActionResult BusStop()
        {
            ViewBag.UserRight = base.UserRight("ScBS");
            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScBS")]
        public ActionResult BusStopList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScBS");

           
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            var data = _scBusStopRepository.GetPagedList(x => x.Id, pageNo, this.SystemControl.PageSize);
            return PartialView("ScBusStopList", data);
        }

        [CheckPermission(Permissions = "Create", Module = "ScBS")]
        public ActionResult AddBusStop()
        {
            return PartialView("ScBusStopAdd");
        }

        [HttpPost]
        public ActionResult AddBusStop(ScBusStop model)
        {
            ScBusStop busStop = _scBusStopRepository.Filter(x => x.Description.Equals(model.Description)).FirstOrDefault();
            if (busStop == null)
            {
                _scBusStopRepository.Add(model);
                return Content("True");
            }
            else
            {
                return Content("Description already exists.");
            }
        }
        [CheckPermission(Permissions = "Edit", Module = "ScBS")]
        public ActionResult BusStopEdit(int id)
        {
            var editBus = _scBusStopRepository.GetById(x => x.Id == id);
            return PartialView(editBus);
        }


        [HttpPost]
        public ActionResult BusStopEdit(ScBusStop model)
        {
            var busStopToEdit = _scBusStopRepository.GetById(x => x.Id == model.Id);
            ScBusStop busStop = _scBusStopRepository.Filter(x => x.Description.Equals(model.Description) && x.Description != busStopToEdit.Description).FirstOrDefault();
            if (busStop == null)
            {
                var _dataContext = new DataContext();
                _dataContext.Entry(model).State = EntityState.Modified;
                _dataContext.SaveChanges();
                return Content("True");
            }
            else
            {
                return Content("Description already exist.");
            }
        }
        [CheckPermission(Permissions = "Delete", Module = "ScBS")]

        public ActionResult DeleteBusStop(int id)
        {

            var data = _scBusStopRepository.DeleteException(x => x.Id == id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //Bus Route Details Actions from here

        [CheckPermission(Permissions = "Navigate", Module = "ScBRD")]
        public ActionResult BusRouteDetails()
        {
            ViewBag.UserRight = base.UserRight("ScBRD");

            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScBRD")]
        public ActionResult BusStopRouteDetailList(int pageNo = 1)
        {

            ViewBag.UserRight = base.UserRight("ScBRD");
          
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;  
            var routeList = _scBusRouteDetailsRepository.GetAll().GroupBy(x => x.BusRouteDescription);
            return PartialView("ScBusRouteDetailsList", routeList.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }

        public BusRouteDetailViewModel BuildBusRouteDetailViewModel()
        {
            var viewmodel = base.CreateViewModel<BusRouteDetailViewModel>();
            return viewmodel;
        }

        public BusTransportationMappingViewModel BuildBusTransportationMappingViewModel()
        {
            var viewmodel = base.CreateViewModel<BusTransportationMappingViewModel>();
            return viewmodel;
        }
        [CheckPermission(Permissions = "Create", Module = "ScBRD")]
        public ActionResult BusRouteDetailsAdd()
        {
            var viewmodel = this.BuildBusRouteDetailViewModel();
            viewmodel.BusRouteDetails = new ScBusRouteDetails();
            var sysCtrl = _systemControlRepository.GetAll().FirstOrDefault();
            if (sysCtrl != null)
            {
                viewmodel.SystemControl = sysCtrl;
            }
            //if (base.SystemControl.DateType == 1)
            //{
            //    if (viewmodel.BusRouteDetails.ApplicableDate != null)
            //    {
            //        viewmodel.DisplayDate = Convert.ToDateTime(viewmodel.BusRouteDetails.ApplicableDate).ToString("MM/dd/yyyy");

            //    }

            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(viewmodel.BusRouteDetails.ApplicableMiti))
            //    {
            //        viewmodel.DisplayDate = viewmodel.BusRouteDetails.ApplicableMiti;

            //    }

            //}
            if (base.SystemControl.IsCurrDate)
            {
                viewmodel.DisplayDate = base.SystemControl.DateType == 1
                                            ? DateTime.Now.ToString("MM/dd/yyyy")
                                            : NepaliDateService.NepaliDate(DateTime.Now).Date;


            }
          
            //viewmodel.BusRouteDetails.ApplicableDate = DateTime.UtcNow;
            viewmodel.PickUpTime = DateTime.UtcNow.ToShortTimeString();
            viewmodel.DropTime = DateTime.UtcNow.ToShortTimeString();
            return PartialView(viewmodel);
        }

        [HttpPost]
        public ActionResult BusRouteDetailsAdd(BusRouteDetailViewModel model, IEnumerable<BusRouteDetailViewModel> busRouteInfoList)
        {
            ScBusRouteDetails busRoute = _scBusRouteDetailsRepository.Filter(x => x.BusRouteDescription.Equals(model.BusRouteDetails.BusRouteDescription)).FirstOrDefault();
            if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(model.DisplayDate))
                {
                    model.BusRouteDetails.ApplicableDate = Convert.ToDateTime(model.DisplayDate);
                    model.BusRouteDetails.ApplicableMiti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayDate)).Date;
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(model.DisplayDate))
                {
                    model.BusRouteDetails.ApplicableMiti = model.DisplayDate;
                    model.BusRouteDetails.ApplicableDate = NepaliDateService.NepalitoEnglishDate(model.BusRouteDetails.ApplicableMiti);
                }
            }


            if (busRoute == null)
            {

                DateTime date= new DateTime();
                string miti = "";
                 if (base.SystemControl.DateType == 1)
            {
                if (!string.IsNullOrEmpty(model.DisplayDate))
                {
                  date= Convert.ToDateTime(model.DisplayDate);
                    miti = NepaliDateService.NepaliDate(Convert.ToDateTime(model.DisplayDate)).Date;
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(model.DisplayDate))
                {
                    miti = model.DisplayDate;
                    date = NepaliDateService.NepalitoEnglishDate(model.BusRouteDetails.ApplicableMiti);
                }
            }
                foreach (var busRouteDetailViewModel in busRouteInfoList)
                {




                    var routeDetail = new ScBusRouteDetails()
                    {
                        BusRouteDescription = model.BusRouteDetails.BusRouteDescription,
                       ApplicableDate = date,
                       ApplicableMiti = miti,
             BusId = model.BusRouteDetails.BusId,
                        Fuel = model.BusRouteDetails.Fuel,
                        BusStopId = busRouteDetailViewModel.BusRouteDetails.BusStopId,
                        Picktime = busRouteDetailViewModel.PickUpTime,
                        Droptime = busRouteDetailViewModel.DropTime,
                        AMOUNT = busRouteDetailViewModel.BusRouteDetails.AMOUNT,
                        Narr = busRouteDetailViewModel.BusRouteDetails.Narr,
                        Remarks = model.BusRouteDetails.Remarks

                    };
                    _scBusRouteDetailsRepository.Add(routeDetail);

                }
                return Content("True");

            }
            else
            {
                return Content("Description already exists.");
            }
        }



        public ActionResult GetBusList()
        {
            var busList = _scBusRepository.GetAll().Select(x => new
            {
                Id = x.Id,
                Description = x.Description
            });
            return Json(busList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBusStopList()
        {
            var busStopList = _scBusStopRepository.GetAll().Select(x => new
            {
                Id = x.Id,
                Description = x.Description
            });
            return Json(busStopList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRouteList()
        {

            IEnumerable<IGrouping<string, ScBusRouteDetails>> routeDetailses = _scBusRouteDetailsRepository.GetAll().OrderBy(x => x.BusRouteDescription).GroupBy(x => x.BusRouteDescription);

            var routes = routeDetailses.Select(x => new
            {
                Id = x.FirstOrDefault().Id,
                Description = x.FirstOrDefault().BusRouteDescription
            });
            return Json(routes, JsonRequestBehavior.AllowGet);
        }
        [CheckPermission(Permissions = "Create", Module = "ScBRD")]
        public ActionResult BusRouteDetailsInfoAdd()
        {
            return PartialView();
        }
        [CheckPermission(Permissions = "Edit", Module = "ScBRD")]
        public ActionResult BusRouteDetailsEdit(string routeDescription)
        {
            var data = _scBusRouteDetailsRepository.GetMany(x => x.BusRouteDescription == routeDescription);

            var viewModel = new BusRouteDetailViewModel();
            viewModel.BusRouteDetails = new ScBusRouteDetails();
            viewModel.BusRouteDetailses = new List<BusRouteDetailViewModel>();
            foreach (var scBusRouteDetailse in data)
            {
                var detailViewModel = new BusRouteDetailViewModel();
                detailViewModel.BusRouteDetails = new ScBusRouteDetails();
                detailViewModel.BusRouteDetails.BusStopId = scBusRouteDetailse.BusStopId;
                detailViewModel.PickUpTime = scBusRouteDetailse.Picktime;
                detailViewModel.DropTime = scBusRouteDetailse.Droptime;
                detailViewModel.BusRouteDetails.AMOUNT = scBusRouteDetailse.AMOUNT;
                detailViewModel.BusRouteDetails.Narr = scBusRouteDetailse.Narr;
                detailViewModel.BusRouteDetails.ScBusStop =
                    _scBusStopRepository.GetById(x => x.Id == scBusRouteDetailse.BusStopId);
                viewModel.BusRouteDetailses.Add(detailViewModel);

            }
            viewModel.BusRouteDetails.BusRouteDescription = data.FirstOrDefault().BusRouteDescription;
            viewModel.BusRouteDetails.ApplicableMiti = data.FirstOrDefault().ApplicableMiti;
            viewModel.BusRouteDetails.ApplicableDate = data.FirstOrDefault().ApplicableDate;
            viewModel.DisplayDate = data.FirstOrDefault().ApplicableDate.ToShortDateString();
            viewModel.BusRouteDetails.BusId = data.FirstOrDefault().BusId;
            viewModel.BusRouteDetails.Fuel = data.FirstOrDefault().Fuel;
            viewModel.BusRouteDetails.Remarks = data.FirstOrDefault().Remarks;
            viewModel.ScBus = data.FirstOrDefault().ScBus;
            viewModel.OldBusDescription = data.FirstOrDefault().BusRouteDescription;
            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult BusRouteDetailsEdit(BusRouteDetailViewModel model, IEnumerable<BusRouteDetailViewModel> busRouteInfoList)
        {
            var busRouteToEdit = _scBusRouteDetailsRepository.GetMany(x => x.BusRouteDescription == model.OldBusDescription);
            var busDescription = busRouteToEdit.FirstOrDefault().BusRouteDescription;
            var busRoute = _scBusRouteDetailsRepository.Filter(x => x.BusRouteDescription.Equals(model.BusRouteDetails.BusRouteDescription) && x.BusRouteDescription != busDescription).FirstOrDefault();
            if (busRoute == null)
            {
                foreach (var scBusRouteDetailse in busRouteToEdit)
                {
                    _scBusRouteDetailsRepository.Delete(scBusRouteDetailse);
                }

                foreach (var busRouteDetailViewModel in busRouteInfoList)
                {
                    var routeDetail = new ScBusRouteDetails()
                    {
                        BusRouteDescription = model.BusRouteDetails.BusRouteDescription,
                        ApplicableDate = model.BusRouteDetails.ApplicableDate,
                        ApplicableMiti = model.BusRouteDetails.ApplicableMiti,
                        BusId = model.BusRouteDetails.BusId,
                        Fuel = model.BusRouteDetails.Fuel,
                        BusStopId = busRouteDetailViewModel.BusRouteDetails.BusStopId,
                        Picktime = busRouteDetailViewModel.PickUpTime,
                        Droptime = busRouteDetailViewModel.DropTime,
                        AMOUNT = busRouteDetailViewModel.BusRouteDetails.AMOUNT,
                        Narr = busRouteDetailViewModel.BusRouteDetails.Narr,
                        Remarks = model.BusRouteDetails.Remarks

                    };
                    _scBusRouteDetailsRepository.Add(routeDetail);

                }
                return Content("True");

            }
            else
            {
                return Content("Description already exists.");
            }
        }

        public ActionResult BusRouteDetailsDelete(string routeDescription)
        {
            _scBusRouteDetailsRepository.Delete(x => x.BusRouteDescription == routeDescription);

            return RedirectToAction("BusRouteDetails");
        }


        //Transport Mapping / Transport Setup actions from here
        [CheckPermission(Permissions = "Navigate", Module = "ScBTM")]
        public ActionResult BusTransportMapping()
        {
            ViewBag.UserRight = base.UserRight("ScBTM");

            return View();
        }
        [CheckPermission(Permissions = "Navigate", Module = "ScBTM")]
        public ActionResult BusTransportMappingList(int pageNo = 1)
        {
            ViewBag.UserRight = base.UserRight("ScBTM");
            var transportMappingList = _scTransportMappingRepository.GetMany(x => x.AcademicYearId == CurrentAcademyYearId).OrderBy(x => x.ClassId).GroupBy(x => x.ClassId);
            var snStart = (pageNo - 1) * this.SystemControl.PageSize + 1;
            ViewBag.SnStart = snStart;
            return PartialView("BusTransportMappingList", transportMappingList.AsQueryable().ToPagedList(pageNo, this.SystemControl.PageSize));
        }
        [CheckPermission(Permissions = "Create", Module = "ScBTM")]
        public ActionResult BusTransportMappingInfoAdd()
        {
            ViewBag.DateType = base.SystemControl.DateType;
            var model = new ScTransportMapping();
            
            
            model.StatusList = new SelectList(
                Enum.GetValues(typeof(Status)).Cast
                    <Status>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
           // model.StartDate = null;
           // model.EndDate = null;
            return PartialView(model);
        }
        [CheckPermission(Permissions = "Edit", Module = "ScBTM")]
        public ActionResult BusTransportMappingInfoEdit()
        {
            ViewBag.DateType = base.SystemControl.DateType;
            var model = new ScTransportMapping();
            model.StatusList = new SelectList(
                Enum.GetValues(typeof(Status)).Cast
                    <Status>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");
            return PartialView(model);
        }

        [CheckPermission(Permissions = "Create", Module = "ScBRD")]
        public ActionResult BusTransportMappingAdd()
        {
            var viewModel = this.BuildBusTransportationMappingViewModel();
            if (base.SystemControl.IsCurrDate)
            {
                 viewModel.DisplayDate = base.SystemControl.DateType == 1
                                            ? DateTime.Now.ToString("MM/dd/yyyy")
                                            : NepaliDateService.NepaliDate(DateTime.Now).Date;
                 viewModel.DisplayEndDate = base.SystemControl.DateType == 1
                                             ? DateTime.Now.ToString("MM/dd/yyyy")
                                             : NepaliDateService.NepaliDate(DateTime.Now).Date;


            }
            viewModel.ScTransportMapping = new ScTransportMapping();
            viewModel.ScTransportMapping.StatusList = new SelectList(
                Enum.GetValues(typeof(Status)).Cast
                    <Status>().Select(
                        x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }), "Value", "Text");

            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult BusTransportMappingAdd(BusTransportationMappingViewModel model, IEnumerable<ScTransportMapping> busTransportMappingInfoList)
        {
            try
            {


                var data =
                    _scTransportMappingRepository.GetMany(
                        x =>
                        x.ClassId == model.ScTransportMapping.ClassId && x.SectionId == model.ScTransportMapping.SectionId && x.AcademicYearId == CurrentAcademyYearId);
                if (!data.Any())
                {
                    foreach (var busTransportMappingInfo in busTransportMappingInfoList)
                    {
                        var transportmapping = new ScTransportMapping()
                                                   {
                                                       ClassId = model.ScTransportMapping.ClassId,
                                                       SectionId = model.ScTransportMapping.SectionId,
                                                       BusRouteCode = model.ScTransportMapping.BusRouteCode,
                                                       StudentId = busTransportMappingInfo.StudentId,
                                                       BusStopFromId = busTransportMappingInfo.BusStopFromId,
                                                       BusStopToId = busTransportMappingInfo.BusStopToId,
                                                       Status = busTransportMappingInfo.Status,
                                                       StartDate = busTransportMappingInfo.StartDate,
                                                       StartMiti = busTransportMappingInfo.StartMiti,
                                                       EndDate = busTransportMappingInfo.EndDate,
                                                       EndMiti = busTransportMappingInfo.EndMiti,
                                                       Narr = busTransportMappingInfo.Narr,
                                                       TransportAmt = busTransportMappingInfo.TransportAmt,
                                                       Remarks = busTransportMappingInfo.Remarks,
                                                       AcademicYearId = CurrentAcademyYearId

                                                   };

                        _scTransportMappingRepository.Add(transportmapping);

                    }
                    return Content("True");
                }
                else
                {
                    return Content("Mapping for this class and section already saved.");
                }
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.InnerException.Message.Split(',')[0]);
            }

        }

        [CheckPermission(Permissions = "Edit", Module = "ScBRD")]
        public ActionResult BusTransportMappingEdit(int classId, int sectionid, string routeid)
        {
            ViewBag.DateType = base.SystemControl.DateType;
            var transportMapping =
                _scTransportMappingRepository.Filter(
                    x => x.ClassId == classId && x.SectionId == sectionid && x.BusRouteCode == routeid && x.AcademicYearId == CurrentAcademyYearId);
            var viewModel = this.BuildBusTransportationMappingViewModel();
            viewModel.ScTransportMapping = new ScTransportMapping();
            viewModel.ScTransportMapping = transportMapping.FirstOrDefault();
            viewModel.ScTransportMappings = transportMapping;
            foreach (var scTransportMapping in viewModel.ScTransportMappings)
            {
                var regNo = UtilityService.GetRegistrationNumberByStudentId(scTransportMapping.StudentId);
                scTransportMapping.RegNo = regNo;
                var startEnd = Convert.ToDateTime(scTransportMapping.StartDate).ToString("MM/dd/yyyy");
            }
            viewModel.OldClassId = classId;
            viewModel.OldSectionId = sectionid;
            viewModel.OldBusRouteCode = routeid;
            return PartialView(viewModel);

        }

        [HttpPost]
        public ActionResult BusTransportMappingEdit(BusTransportationMappingViewModel model, IEnumerable<ScTransportMapping> busTransportMappingInfoList)
        {
            try
            {


                var data =
              _scTransportMappingRepository.GetMany(
                  x =>
                  x.ClassId == model.ScTransportMapping.ClassId && x.AcademicYearId == CurrentAcademyYearId && x.SectionId == model.ScTransportMapping.SectionId && x.ClassId != model.OldClassId && x.SectionId != model.OldSectionId);
                if (!data.Any())
                {

                    _scTransportMappingRepository.Delete(
                        x =>
                        x.ClassId == model.OldClassId && x.SectionId == model.OldSectionId &&
                        x.BusRouteCode == model.OldBusRouteCode && x.AcademicYearId == CurrentAcademyYearId);


                    foreach (var item in busTransportMappingInfoList.Where(x => x.StudentId != 0))
                    {
                        var transportMapping = new ScTransportMapping()
                        {
                            ClassId = model.ScTransportMapping.ClassId,
                            SectionId = model.ScTransportMapping.SectionId,
                            BusRouteCode = model.ScTransportMapping.BusRouteCode,
                            StudentId = item.StudentId,
                            BusStopFromId = item.BusStopFromId,
                            BusStopToId = item.BusStopToId,
                            Status = item.Status,
                            StartDate = item.StartDate,
                            StartMiti = item.StartMiti,
                            EndDate = item.EndDate,
                            EndMiti = item.EndMiti,
                            Narr = item.Narr,
                            TransportAmt = item.TransportAmt,
                            Remarks = model.ScTransportMapping.Remarks,
                            AcademicYearId = CurrentAcademyYearId
                        };
                        _scTransportMappingRepository.Add(transportMapping);
                    }
                    return Content("True");
                }
                else
                {
                    return Content("Mapping for this class and section already saved.");
                }
            }
            catch (Exception ex)
            {
                return Content(ex.InnerException.InnerException.Message.Split(',')[0]);
            }
        }


        public ActionResult BusTransportMappingDelete(int classId, int sectionid, string routeid)
        {
            _scTransportMappingRepository.Delete(x => x.ClassId == classId && x.AcademicYearId == CurrentAcademyYearId && x.SectionId == sectionid && x.BusRouteCode == routeid);


            return RedirectToAction("BusTransportMapping");
        }


        public ActionResult StudentRouteByClassId(int classid, int pageSize = 1)
        {
            var data = _scTransportMappingRepository.GetMany(x => x.ClassId == classid).ToList();
            //Session["ReportModel"] = classid;
            return PartialView("BusTransportationMappingInfoList", data.AsQueryable().ToPagedList(pageSize, this.SystemControl.PageSize));

        }

        public ActionResult GetStudentsByClassAndSection(int classId, int sectionId)
        {
            ViewBag.DateType = base.SystemControl.DateType;
            //var data = _scStudentinfoRepository.GetMany(x => x.ClassId == classId && x.SectionId == sectionId);

            Expression<Func<ScStudentRegistrationDetail, bool>> predicate = x => true;
            predicate = predicate.And(x => x.StudentRegistration.ClassId == classId);
            predicate = predicate.And(x => x.StudentRegistration.AcademicYearId == CurrentAcademyYearId);
            if (sectionId != 0)
            {
                predicate = predicate.And(x => x.SectionId == sectionId);
            }
            var data = _scStudentRegistrationDetailRepository.GetExpandable(predicate);
            var student1 = new List<ScStudentinfo>();
            if (data.Any())
            {
                student1 = data.Select(x => x.Studentinfo).ToList();
            }


            List<ScTransportMapping> liststudents = new List<ScTransportMapping>();
            foreach (var item in student1)
            {
                var student = new ScTransportMapping()
                {
                    StudentId = item.StudentID,
                    RegNo = item.Regno,

                };
                student.studentinfo = _scStudentinfoRepository.GetById(x => x.StudentID == item.StudentID);
                student.StartDate = null;
                student.EndDate = null;
                liststudents.Add(student);
            }

            return PartialView("BusTransportMappingInfoDetail", liststudents);
        }


        public ActionResult PdfBusTransportMapping(int classid)
        {
            var data = _scTransportMappingRepository.GetMany(x => x.ClassId == classid).ToList();
            return this.ViewPdf("", "Transportation/PdfBusTransportMapping", data);
        }
        public ActionResult ExcelBusTransportMapping(int classid)
        {
            var data = _scTransportMappingRepository.GetMany(x => x.ClassId == classid).ToList();
            return this.ViewExcel("", "Transportation/ExcelBusTransportMapping", data);
        }
        public ActionResult Test()
        {
            var obj = new Design();
            
            var prop = obj.GetType();
         foreach (var f in prop.GetProperties())
            {
               var dd= f.Name;
            }
            return null;
        }


    }
}
