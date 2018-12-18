using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Academy
{
    public class BusRouteDetailViewModel :BaseViewModel
    {
        public ScBusRouteDetails BusRouteDetails { get; set; }
        public SystemControl SystemControl { get; set; }
        public ScBus ScBus { get; set; }
        public string DisplayDate { get; set; }
        public List<BusRouteDetailViewModel> BusRouteDetailses { get; set; }
        public string PickUpTime { get; set; }
        public string DropTime { get; set; }
        public string OldBusDescription { get; set; }

       
    }
}