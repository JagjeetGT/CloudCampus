using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels.StudentProfile
{
    public class StudentLibraryHistory
    {
        public int IssueId { get; set; }
        public DateTime IssueDate { get; set; }
        public int BookDetailId { get; set; }
        public string CardName { get; set; }
        public bool IsReturn { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal? Fine { get; set; }
        public string BookName { get; set; }
        public string Publisher { get; set; }
        public string Edition { get; set; }

}
}