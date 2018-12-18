using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.Home
{
    public class DashboardLoanViewModel : BaseViewModel
    {
        public DateTime CurrentDate { get; set; }
        public IEnumerable<SP_LoanMaturation> MaturedLoans { get; set; }
        public IEnumerable<SP_LoanMaturation> DueLoans { get; set; }
        public IEnumerable<SP_LoanMaturation> UpcomingLoans { get; set; }
    }
}