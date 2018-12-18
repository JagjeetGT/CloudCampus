using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Academy
{
    public class AdcamicsStudentFeeRateViewModel : BaseViewModel 
    {
        public ScStudentFeeRateMaster StudentFeeRateMaster { get; set; }
        public IEnumerable<ScStudentFeeRateDetail> ScStudentFeeRateDetails { get; set; }
       
    }
}