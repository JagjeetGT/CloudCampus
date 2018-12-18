using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Service.Helpers;

namespace KRBAccounting.Web.ViewModels.Transaction
{
    public class MonthlyBillGenerationViewModel : BaseViewModel
    {
        public ScMonthlyBillStudent MonthlyBill { get; set; }
        public IList<CheckBoxListInfo> MonthlyFeeList { get; set; }
        public IList<CheckBoxListInfo> AdmissionFeeList { get; set; }
       
        public List<CheckBoxListInfo> ExamFeeList { get; set; }
        public List<CheckBoxListInfo> OtherList { get; set; }
        public  IList<CheckBoxListInfo> MonthList  { get; set; }
        public SelectList MonthLists { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}