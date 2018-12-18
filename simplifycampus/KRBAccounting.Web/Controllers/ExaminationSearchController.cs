using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Web.ViewModels.Academy;

namespace KRBAccounting.Web.Controllers
{
    public class ExaminationSearchController : SearchBaseController
    {
        //
        // GET: /ExaminationSearcgh/


        #region Constructor

        private readonly IScExamRepository _scExamRepository;
        private readonly IScConsolidatedMarksSetupRepository _scConsolidatedMarksSetupRepository;

        public  ExaminationSearchController(IScExamRepository scExamRepository,
            IScConsolidatedMarksSetupRepository scConsolidatedMarksSetupRepository)
        {

            _scExamRepository = scExamRepository;
            _scConsolidatedMarksSetupRepository = scConsolidatedMarksSetupRepository;
        }

        #endregion 
        public ActionResult Index()
        {
            return View();
        }


        #region ExamSearch


        [HttpPost]
        public ActionResult ExamSearch(string SearchText)
        {

            ViewBag.UserRight = base.UserRight("Exam");
            ViewBag.Datetype = base.SystemControl.DateType;
            var list = _scExamRepository.GetMany(x => x.Description.Contains(SearchText) || x.Code.Contains(SearchText));

            return PartialView("_PartialExamSearchList", list.OrderByDescending(x => x.Id));
        }


        #endregion 

        #region ConsolidatedMarksSetups

        [HttpPost]
        public ActionResult ConsolidatedMarksSetupsSearch(string SearchText)
        {

            ViewBag.UserRight = base.UserRight("ScCMS");
          
           

            ViewBag.UserRight = base.UserRight("ScCMS");
        //    var list = _scConsolidatedMarksSetupRepository.GetMany(x => x.Description.Contains(SearchText) || x.Code.Contains(SearchText));
            var list = from d in _scConsolidatedMarksSetupRepository.GetMany(x => x.Description.Contains(SearchText) || x.Code.Contains(SearchText))
                                   group d by new { d.ClassId, d.ExamId }
                                       into g
                                       select g.ToList();
            return PartialView("_PartialConsolidatedMarksSetupsSearchList", list);
        }

        #endregion 

    }
}
