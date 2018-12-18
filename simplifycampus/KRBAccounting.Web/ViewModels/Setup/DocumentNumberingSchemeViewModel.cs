using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Setup
{
    public class DocumentNumberingSchemeViewModel
    {
        public DocumentNumberingScheme DocumentNumberingScheme { get; set; }
        public SelectList Module { get; set; }
        public SelectList Type { get; set; }
        public SelectList Mode { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        
    }
}