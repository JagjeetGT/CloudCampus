using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Academy
{
    public class BusTransportationMappingViewModel : BaseViewModel
    {
        public ScTransportMapping ScTransportMapping { get; set; }
        public IEnumerable<ScTransportMapping> ScTransportMappings { get; set; }

        public string DisplayDate { get; set; }
        public string DisplayEndDate { get; set; }
        public int RegNo { get; set; }
        public int OldClassId { get; set; }
        public int OldSectionId { get; set; }
        public string OldBusRouteCode { get; set; }
    }
}