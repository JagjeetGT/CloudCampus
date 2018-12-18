using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Library
{
    public class LibraryBookIssueViewModel
    {
        public ScBookIssued BookIssued { get; set; }
        public string BookTitle { get; set; }
        public ScBookReceivedDetails BookReceivedDetails { get; set; }
        public string Name { get; set; }
        public string BookNo { get; set; }
        public string CardNo { get; set; }
        public int CardId { get; set; }
        public string CardName { get; set; }
        [NotMapped]
        public List<SelectListItem> Typelist { get; set; }
        public IEnumerable<ScLibraryCard> ScLibraryCards { get; set; }
    }
}