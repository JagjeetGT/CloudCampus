using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Data.Repositories;

namespace KRBAccounting.Web.Controllers
{
    public class TransportationSearchController : SearchBaseController
    {
        //
        // GET: /TransportationSearch/
        #region Constructor

        private readonly IScBusRepository _scBusRepository;
        private readonly IScBusStopRepository _scBusStopRepository;
        private readonly IScBusRouteDetailsRepository _scBusRouteDetailsRepository;
        public TransportationSearchController(IScBusRepository scBusRepository,
            IScBusStopRepository scBusStopRepository,
            IScBusRouteDetailsRepository scBusRouteDetailsRepository)
        {
            _scBusRepository = scBusRepository;
            _scBusStopRepository = scBusStopRepository;
            _scBusRouteDetailsRepository = scBusRouteDetailsRepository;
        }

        #endregion
        public ActionResult Index()
        {
            return View();
        }


        #region BusSearch
        [HttpPost]
        public ActionResult BusSearch(string SearchText)
        {
            ViewBag.UserRight = base.UserRight("ScB");

            var list = _scBusRepository.GetMany(x => x.Description.Contains(SearchText) || x.VehicleNo.Contains(SearchText) || x.DriverName.Contains(SearchText) ||
                x.ContactPerson.Contains(SearchText));

            return PartialView("_PartialBusSearchList", list.OrderByDescending(x => x.Id));
        }

        #endregion

        #region Bus Stop Search

        [HttpPost]
        public ActionResult BusStopSearch(string SearchText)
        {
            ViewBag.UserRight = base.UserRight("ScBS");
            var list = _scBusStopRepository.GetMany(x => x.Description.Contains(SearchText) || x.Memo.Contains(SearchText));

            return PartialView("_PartialBusStopSearchList", list.OrderByDescending(x => x.Id));
        }

        #endregion

        #region Bus Route Details
        [HttpPost]
        public ActionResult BusRouteDetailSearch(string SearchText)
        {

            ViewBag.UserRight = base.UserRight("ScBRD");
        
            var routeList = _scBusRouteDetailsRepository.GetMany(x => x.BusRouteDescription.Contains(SearchText)).GroupBy(x => x.BusRouteDescription);

            return PartialView("_PartialBusRouteDetailSearchList", routeList);
        }

        #endregion
    }
}
