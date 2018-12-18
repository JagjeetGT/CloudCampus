using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data.Repositories;
using KRBAccounting.Web.CustomProviders;

namespace KRBAccounting.Web.Controllers
{
    public class AdministrationSearchController : SearchBaseController
    {
        #region Constructor

        private readonly IScBoaderRepository _scBoaderRepository;
        private readonly IScFeeItemRepository _scFeeItemRepository;
        private readonly IFormsAuthenticationService _authentication;
        private readonly IScFeeTermRepository _scFeeTermRepository;
        private readonly IScHouseGroupRepository _scHouseGroupRepository;
        private readonly ISchBuildingRepository _schBuildingRepository;
        private readonly IScRoomRepository _scRoomRepository;
       

        public AdministrationSearchController(IScBoaderRepository scBoaderRepository,
            IScFeeItemRepository scFeeItemRepository,
            IScFeeTermRepository scFeeTermRepository,
            IScHouseGroupRepository scHouseGroupRepository,
                 ISchBuildingRepository schBuildingRepository,
            IScRoomRepository scRoomRepository)
        {

            _scBoaderRepository = scBoaderRepository;
            _scFeeItemRepository = scFeeItemRepository;
            _scFeeTermRepository = scFeeTermRepository;
            _scHouseGroupRepository = scHouseGroupRepository;
            _schBuildingRepository = schBuildingRepository;
            _scRoomRepository = scRoomRepository;
            _authentication = new FormsAuthenticationService(new HttpContextWrapper(System.Web.HttpContext.Current));
        }

        #endregion
        //
        // GET: /AdministrationSearch/

        public ActionResult Index()
        {
            return View();
        }


        #region BoaderSearch
        [HttpPost]
        public ActionResult BoaderSearch(string SearchText)
        {
            ViewBag.UserRight = base.UserRight("ScBO");
            var list = _scBoaderRepository.GetMany(x => x.Description.Contains(SearchText) || x.Code.Contains(SearchText));

            return PartialView("_PartialBoaderSearchList", list.OrderByDescending(x => x.Id));
        }
        #endregion

        #region FeeItemSearch

        [HttpPost]
        public ActionResult FeeItemSearch(string SearchText)
        {
            ViewBag.UserRight = base.UserRight("ScFI");
            var list = _scFeeItemRepository.GetMany(x => x.Description.Contains(SearchText) || x.Code.Contains(SearchText));

            return PartialView("_PartialFeeItemSearchList", list.OrderByDescending(x => x.Id));
        }

        #endregion 

        #region FeeTermSearch

        [HttpPost]
        public ActionResult FeeTermSearch(string SearchText)
        {
            ViewBag.UserRight = base.UserRight("ScFT");
            var list = _scFeeTermRepository.GetMany(x => x.Description.Contains(SearchText) || x.Code.Contains(SearchText));

            return PartialView("_PartialFeeTermSearchList", list.OrderByDescending(x => x.Id));
        }

        #endregion

        #region HouseGroupSearch


        [HttpPost]
        public ActionResult HouseGroupSearch(string SearchText)
        {
            ViewBag.UserRight = base.UserRight("ScHG");
            var list = _scHouseGroupRepository.GetMany(x => x.Description.Contains(SearchText) || x.Code.Contains(SearchText));

            return PartialView("_PartialHouseGroupSearchList", list.OrderByDescending(x => x.Id));
        }


        #endregion 

        #region Building Search
        [HttpPost]
        public ActionResult BuildingSearch(string SearchText)
        {
            ViewBag.UserRight = base.UserRight("ScBlD");
            var list = _schBuildingRepository.GetMany(x => x.Description.Contains(SearchText) || x.Code.Contains(SearchText));

            return PartialView("_PartialBuildingSearchList", list.OrderByDescending(x => x.Id));
        }

        #endregion

        #region Room Search
        [HttpPost]
        public ActionResult RoomSearch(string SearchText)
        {
            ViewBag.UserRight = base.UserRight("ScRoom");
            var list = _scRoomRepository.GetMany(x => x.Description.Contains(SearchText) || x.Code.Contains(SearchText) || x.Memo.Contains(SearchText));

            return PartialView("_PartialRoomSearchList", list.OrderByDescending(x => x.Id));
        }


        #endregion 

    }
}
