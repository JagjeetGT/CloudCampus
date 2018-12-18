using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{    public class ScTransportMapping
{
        [Key]
        public int Id {get;set;}
        [Required(ErrorMessage =  "*" )]
        public int ClassId {get;set;}
        [Required(ErrorMessage =  "*" )]
        public int SectionId {get;set;}
        [Required(ErrorMessage =  "*" )]
        public string BusRouteCode {get;set;}
        public int StudentId {get;set;}
        public int BusStopFromId {get;set;}
        public int BusStopToId {get;set;}
        public string Status {get;set;}

        //[RegularExpression(@"^(0[1-9]|1[012])[/](0[1-9]|[12][0-9]|3[01])[/]\d{4}$", ErrorMessage = " ")]/*mm-dd-yyyy*/
        [DataType(DataType.Date)]
        public DateTime? StartDate {get;set;}

        //[RegularExpression(@"^(0[1-9]|1[012])[/](0[1-9]|[12][0-9]|3[01])[/]\d{4}$", ErrorMessage = " ")]/*mm-dd-yyyy*/
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [RegularExpression(@"^\d{4}[\/\-](0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[02])$", ErrorMessage = " ")]/*yyyy-mm-yyy*/
        public string StartMiti {get;set;}
        [RegularExpression(@"^\d{4}[\/\-](0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[02])$", ErrorMessage = " ")]/*yyyy-mm-yyy*/
        public string EndMiti { get; set; }
        public string Narr {get;set;}
        [Required(ErrorMessage =  "*" )]
        public decimal TransportAmt {get;set;}
        public string Remarks {get;set;}
        public int AcademicYearId { get; set; }
       


        [ForeignKey("StudentId")]
        public virtual ScStudentinfo studentinfo { get; set; }

        [ForeignKey("ClassId")]
        public virtual SchClass SchClass { get; set; }

        [ForeignKey("SectionId")]
        public virtual ScSection ScSection { get; set; }

      [ForeignKey("BusStopFromId")]
        public virtual ScBusStop ScBusStopFrom { get; set; }

      [ForeignKey("BusStopToId")]
      public virtual ScBusStop ScBusStopTo { get; set; }

    


        
        [NotMapped]
        public SelectList StatusList { get; set; }

        [NotMapped]
        public string RegNo { get; set; }
    }
}
