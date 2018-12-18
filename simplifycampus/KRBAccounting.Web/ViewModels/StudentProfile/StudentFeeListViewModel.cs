using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.StudentProfile
{
    public class StudentFeeListViewModel
    {
        public IEnumerable<ScBillTransaction> BillTransactions { get; set; }
        public IEnumerable<ScMonthlyBill> MonthlyBills { get; set; }
    }
}