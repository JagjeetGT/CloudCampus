using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Service.Models;
using KRBAccounting.Web.CustomProviders;

namespace KRBAccounting.Web.Controllers
{
    public class AcademySearchController : SearchBaseController
    {
        //
        // GET: /AcademySearch/
        #region Constructor

        private readonly IScProgramMasterRepository _scProgramMasterRepository;
        private readonly IScClassRepository _scClassRepository;
        private readonly IScSectionRepository _scSectionRepository;
        private readonly IFormsAuthenticationService _authentication;
        private readonly IScShiftRepository _scShiftRepository;
        private readonly IScSubjectRepository _scSubjectRepository;
        private readonly IScGradeRepository _scGradeRepository;
        private readonly IScDivisionRepository _scDivisionRepository;
        private readonly IScExtraActivityRepository _scExtraActivityRepository;
        private readonly ISchBuildingRepository _schBuildingRepository;
        private readonly IScRoomRepository _scRoomRepository;
       


        public AcademySearchController(IScProgramMasterRepository scProgramMasterRepository,
            IScClassRepository scClassRepository,
            IScSectionRepository scSectionRepository,
            IScShiftRepository scShiftRepository,
            IScSubjectRepository scSubjectRepository,
            IScGradeRepository scGradeRepository,
            IScDivisionRepository scDivisionRepository,
            IScExtraActivityRepository scExtraActivityRepository,
            ISchBuildingRepository schBuildingRepository,
            IScRoomRepository scRoomRepository)
        {

            _scProgramMasterRepository = scProgramMasterRepository;
            _scClassRepository = scClassRepository;
            _scSectionRepository = scSectionRepository;
            _scShiftRepository = scShiftRepository;
            _scSubjectRepository = scSubjectRepository;
            _scGradeRepository = scGradeRepository;
            _scDivisionRepository = scDivisionRepository;
            _scExtraActivityRepository = scExtraActivityRepository;
            _schBuildingRepository = schBuildingRepository;
            _scRoomRepository = scRoomRepository;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
        }

        #endregion

        public ActionResult Index()
        {
            return View();
        }

        #region ProgramMasterSearch

        [HttpPost]
        public ActionResult ProgramMasterSearchList(string SearchText)
        {
            var list = _scProgramMasterRepository.GetMany(x => x.Description.Contains(SearchText) || x.Code.Contains(SearchText) || x.Incharge.Contains(SearchText) ||
                x.FacultyDescription.Contains(SearchText) || x.LevelDescription.Contains(SearchText) || x.UniversityDescription.Contains(SearchText));

            return PartialView("_PartialProgramMasterSearchList", list.OrderByDescending(x => x.Id));
        }

        #endregion

        #region ClassSearch
        [HttpPost]
        public ActionResult ClassSearch(string SearchText)
        {
            ViewBag.UserRight = base.UserRight("ScCls");
            var list = _scClassRepository.GetMany(x => x.Description.Contains(SearchText) || x.Code.Contains(SearchText) || x.Incharge.Contains(SearchText));

            return PartialView("_PartialClassSearchList", list.OrderByDescending(x => x.Id));
        }
        #endregion

        #region SectionSearch

        [HttpPost]
        public ActionResult SectionSearch(string SearchText)
        {
            ViewBag.UserRight = base.UserRight("ScSec");
           
          

            var list = _scSectionRepository.GetMany(x => x.Description.Contains(SearchText) || x.Memo.Contains(SearchText));

            return PartialView("_PartialSectionSearchList", list.OrderByDescending(x => x.Id));
        }

        #endregion

        #region ShiftSearch

        [HttpPost]
        public ActionResult ShiftSearch(string SearchText)
        {

            ViewBag.UserRight = base.UserRight("ScSh");
            var list =
                _scShiftRepository.GetMany(x =>x.Description.Contains(SearchText) || x.Code.Contains(SearchText) || x.Memo.Contains(SearchText));

            return PartialView("_PartialShiftSearchList", list.OrderByDescending(x => x.Id));
        }

        #endregion

        #region SubjectSearch

        [HttpPost]
        public ActionResult SubjectSearch(string SearchText)
        {
            ViewBag.UserRight = base.UserRight("ScSub");
      


            var list =
                _scSubjectRepository.GetMany(x => x.Description.Contains(SearchText) || x.Code.Contains(SearchText));
            return PartialView("_PartialSubjectSearchList", list.OrderByDescending(x => x.Id));
        }


        #endregion

        #region GradingSearch

        [HttpPost]
        public ActionResult GradingSearch(string SearchText)
        {
            ViewBag.UserRight = base.UserRight("ScG");
            var list = _scGradeRepository.GetMany(x => x.Code.Contains(SearchText) || x.Code.Contains(SearchText) || x.Grade.Contains(SearchText) ||
                x.Memo.Contains(SearchText));

            return PartialView("_PartialGradingSearchList", list.OrderByDescending(x => x.Id));
        }

        #endregion

        #region DivisionSearch
        [HttpPost]
        public ActionResult DivisionSearch(string SearchText)
        {
            ViewBag.UserRight = base.UserRight("ScD");
            var list = _scDivisionRepository.GetMany(x => x.Description.Contains(SearchText) || x.Memo.Contains(SearchText));

            return PartialView("_PartialDivisionSearchList", list.OrderByDescending(x => x.Id));
        }



        #endregion 

        #region ExtraActivitySearch
        [HttpPost]
        public ActionResult ExtraActivitySearch(string SearchText)
        {
            ViewBag.UserRight = base.UserRight("scEA");
            var list = _scExtraActivityRepository.GetMany(x => x.Description.Contains(SearchText) || x.Code.Contains(SearchText) || x.Memo.Contains(SearchText));

            return PartialView("_PartialExtraActivitySearchList", list.OrderByDescending(x => x.Id));
        }

        #endregion

        

    }
}
