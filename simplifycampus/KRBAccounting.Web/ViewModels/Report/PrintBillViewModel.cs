using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.Report
{
    public class BillChild 
    {
        public string ItemName { get; set; }
        public decimal Amount { get; set; }
        public decimal TermAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Total { get; set; }
        public decimal TaxAmount { get; set; }
    }
   public class BillParent
   {
       public string StudentName { get; set; }
       public string ClassName { get; set; }
       public string SectionName { get; set; }
       public string BillFor { get; set; }
       public string RollNo { get; set; }
       public string BillNo { get; set; }
       public string BillDate { get; set; }
       public decimal Total { get; set; }
       public decimal Amount { get; set; }
       public string AmountInWord { get; set; }
       public string PreparedBy { get; set; }
       public decimal EducationTax { get; set; }
       public List<BillChild> Children { get; set; }
       public ReportHeader Header { get; set; }
       public decimal DueAmount { get; set; }
   }
    public class PreviousDue
    {
        public int StudentId { get; set; }
        public decimal Amount { get; set; }
        
    }
}