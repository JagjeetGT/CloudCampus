using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScBookIssueReturn
    {
        [Key]
        public int Id { get; set; }

        public int CardId { get; set; }
        public int BookDetailId { get; set; }
        public decimal FineAmount { get; set; }
        public DateTime ReturnDate { get; set; }
        public string ReturnMiti { get; set; }
        public int CreatedById { get; set; }
        public int IssueId { get; set; }

        public int AcademyYearId { get; set; }
        [ForeignKey("BookDetailId")]
        public virtual ScBookReceivedDetails ReceivedDetails { get; set; }

        [ForeignKey("IssueId")]
        public virtual ScBookIssued BookIssued { get; set; }
        [ForeignKey("CardId")]
        public virtual ScLibraryCard LibraryCard { get; set; }


    }
}
