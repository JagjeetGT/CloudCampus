using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.StoredProcedures
{
    public class SP_LibraryInformation
    {
        public int StudentId { get; set; }
        public string Student { get; set; }
        public string LibRegNo { get; set; }

        public string CardNo { get; set; }
        public bool CardStatus { get; set; }
        public DateTime Date { get; set; }
        public string Miti { get; set; }
        public DateTime BookDueDate { get; set; }
        public string BookDueMiti { get; set; }
        public bool Return { get; set; }

        public string AccessionNo { get; set; }

        public string Class { get; set; }
        public string StdCode { get; set; }
        public string RollNo { get; set; }
        public string Regno { get; set; }
        public string Section { get; set; }
        public string TmpAdd { get; set; }
        public string TmpCity { get; set; }
        public string TmpDistrict { get; set; }
        public string TmpCountry { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string ReturnMiti { get; set; }
        public decimal? FineAmount { get; set; }
        public int? IsReturn { get; set; }
        public int? DueDay { get; set; }
        public decimal? FinedAmount { get; set; }
        public decimal? DueAmt { get; set; }
        public string BookName { get; set; }
    }
}
