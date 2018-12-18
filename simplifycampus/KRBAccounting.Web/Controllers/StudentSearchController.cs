using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.Controllers
{
    public class StudentSearchController : SearchBaseController
    {
        //
        // GET: /StudentSearch/

        #region Constructor

        private readonly  IScStudentCategoryRepository _scStudentCategory;
        private readonly IScStudentinfoRepository _scStudentinfoRepository;
        private readonly IScFeeReceiptRepository _scFeeReceiptRepository;

        public StudentSearchController(
            IScStudentCategoryRepository scStudentCategoryRepository,
            IScStudentinfoRepository scStudentinfoRepository,
            IScFeeReceiptRepository scFeeReceiptRepository

           
            )
        {


            _scStudentCategory = scStudentCategoryRepository;
            _scStudentinfoRepository = scStudentinfoRepository;
            _scFeeReceiptRepository = scFeeReceiptRepository;

        }
        #endregion 

        public ActionResult Index()
        {
            return View();
        }


        #region Student Category Search
        [HttpPost]
        public ActionResult StudentCategorySearch(string SearchText)
        {
            ViewBag.UserRight = base.UserRight("ScSC");
            var list = _scStudentCategory.GetMany(x => x.Description.Contains(SearchText) || x.Code.Contains(SearchText));

            return PartialView("_PartialStudentCategorySearchList", list.OrderByDescending(x => x.Id));
        }

        #endregion 

        #region Student Info Search
        [HttpPost]
        public ActionResult StudentSearch(string SearchText)
        {
            ViewBag.UserRight = base.UserRight("ScStinfo");
            var list = _scStudentinfoRepository.GetMany(x => x.StuDesc.Contains(SearchText) || x.StdCode.Contains(SearchText));

            return PartialView("_PartialStudentSearchList", list.OrderByDescending(x => x.StuDesc));
        }

        #endregion 

        #region Fee Receipt
        [HttpPost]
        public ActionResult FeeReceiptSearch(string SearchText)
        {
            var list = _scFeeReceiptRepository.GetMany(x => x.ReceiptNo.Contains(SearchText));

            return PartialView("_PartialFeeReceiptSearchList", list.OrderByDescending(x => x.Id));
        }

        #endregion
    }
}
