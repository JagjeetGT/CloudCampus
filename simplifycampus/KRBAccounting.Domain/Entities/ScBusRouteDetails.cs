using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class ScBusRouteDetails
        public string Droptime { get; set; }

        [ForeignKey("BusId")]
        public virtual ScBus ScBus { get; set; }

        [ForeignKey("BusStopId")]
        public virtual ScBusStop ScBusStop { get; set; }

       