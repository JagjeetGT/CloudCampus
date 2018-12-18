using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Management
{
    public class RollNumberingSchemeViewModel
    {
        public ScRollNumberingScheme RollNumberingScheme { get; set; }
        public SelectList ClassList { get; set; }
        public List<SelectListItem> SectionList { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public SelectList Mode { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime StartDate { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime EndDate { get; set; }
    }
}