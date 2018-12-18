using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.Academy
{
    public class AdcademicsClassFeeRateViewModel : BaseViewModel

    {
        public ScClassFeeRate ClassFeeRate { get; set; }
        public IEnumerable<IGrouping<int, ScClassFeeRate>> ClassFeeRateGrouping { get; set; }
        public IEnumerable<ScClassFeeRate> ClassFeeRates { get; set; }
        public string ClassCode { get; set; }
        public decimal TotalAmount { get; set; }
        public int ClassId { get; set; }
        public int ShiftId { get; set; }
        public int BoaderId { get; set; }
    }
}