using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class ScBusRouteDetails{        [Key]        public int Id {get;set;}        [Required(ErrorMessage =  " " )]        public string BusRouteDescription {get;set;}        public DateTime ApplicableDate {get;set;}        public string ApplicableMiti {get;set;}        public int BusId {get;set;}        public string Fuel {get;set;}        public int BusStopId {get;set;}        public string Picktime {get;set;}
        public string Droptime { get; set; }        public decimal AMOUNT {get;set;}        public string Narr {get;set;}        public string Remarks {get;set;}

        [ForeignKey("BusId")]
        public virtual ScBus ScBus { get; set; }

        [ForeignKey("BusStopId")]
        public virtual ScBusStop ScBusStop { get; set; }

           }}