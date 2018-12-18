using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScLibrarySetting
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public int BookDueDate { get; set; }

        public bool IsResgistrationNumberUse { get; set; }
        public int TotalCardIssue { get; set; }
        [NotMapped]
        public string Date { get; set; }

        [NotMapped]
        public int DateType { get; set; }

    }
}
