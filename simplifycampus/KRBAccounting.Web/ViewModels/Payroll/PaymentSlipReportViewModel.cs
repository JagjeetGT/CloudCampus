using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Payroll
{
    public class PaymentSlipReportViewModel : ReportBaseViewModel
    {
        public IEnumerable<PyPaymentSlip> PaymentSlips;
    }
}