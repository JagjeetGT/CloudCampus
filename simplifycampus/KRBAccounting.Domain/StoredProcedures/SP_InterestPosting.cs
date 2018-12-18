using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_InterestPosting
    {
        public int GlCode { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal InterestRate { get; set; }
        public string InterestPosting { get; set; }
        public string AccNo { get; set; }
        public string AccHolderName { get; set; }
        public int AccountId { get; set; }
    }

    public class SP_InterestPostingCalc
    {
        public int AccountId { get; set; }
        public string AccNo { get; set; }
        public string AccHolderName { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal InterestAmt { get; set; }
        public decimal TDSAmt { get; set; }
        public decimal Balance { get; set; }
        public string InterestCalcBase { get; set; }
    }

    public class SP_InterestCalculationOnDailyBasic
    {
        public int GlCode { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal InterestAmt { get; set; }
        public decimal InterestRate { get; set; }
        public string InterestPosting { get; set; }
        public int AccountId { get; set; }
        public string AccHolderName { get; set; }
    }
    public class SP_InterestCalcOnDailyBasicForRemaining
    {
        public int GlCode { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal InterestAmt { get; set; }
        public decimal InterestRate { get; set; }
        public string InterestPosting { get; set; }
        public int AccountId { get; set; }
        public string AccHolderName { get; set; }

    }
    public class SP_UpdateDailyInterestCalculation
    {
        public DateTime CalculatedDate { get; set; }
        public bool IsInterestPosting { get; set; }
    }
}
