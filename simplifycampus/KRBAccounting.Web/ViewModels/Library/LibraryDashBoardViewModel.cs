using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Library
{
    public class LibraryDashBoardViewModel : BaseViewModel

    {
        public IEnumerable<ScBook> NewArrival { get; set; }
        public IEnumerable<ScBookReceivedDetails> SearchBook { get; set; }
        public IEnumerable<ScBookIssued> DueBookIssueds { get; set; }
        public decimal AvailableBooks { get; set; }
        public decimal NotAvailableBooks { get; set; }
    }
}