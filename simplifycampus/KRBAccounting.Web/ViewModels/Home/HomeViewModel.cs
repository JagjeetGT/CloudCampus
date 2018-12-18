using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.StoredProcedures;

namespace KRBAccounting.Web.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        public CashFlowChart CashFlowInChart { get; set; }
        public CashFlowChart CashFlowOutChart { get; set; }
        public DateTime CurrentDate { get; set; }

        public AnalysisChart MonthlyAnalysisChart { get; set; }
        public AnalysisChart YearlyAnalysisChart { get; set; }

        public CashAnalysisChart CashAnalysisChart { get; set; }
        public CashAnalysisChart BankAnalysisChart { get; set; }
        public List<decimal> CashAnalysis { get; set; }
        public List<PieViewModel> TopItem { get; set; }
        public List<PieViewModel> TopCustomer { get; set; }
        public List<SP_PLTotalForDashBoard> PLTotalList { get; set; }
        public List<SP_AccountWatchList> AccountWatchLists { get; set; }
    }
}