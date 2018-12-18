using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ScBookIssued
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Miti { get; set; }
        public int BookReceivedDetailId { get; set; }
        public DateTime BookDueDate { get; set; }
        public string BookDueMiti { get; set; }
        public int CardId { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AcademyYearId { get; set; }
        public bool IsReturn { get; set; }
        [ForeignKey("BookReceivedDetailId")]
        public virtual ScBookReceivedDetails BookReceivedDetails { get; set; }
        [ForeignKey("CardId")]
        public virtual ScLibraryCard LibraryCard { get; set; }
        [NotMapped]
        public string BookNo { get; set; }
        [NotMapped]
        public decimal FineAmount { get; set; }
    }
}
