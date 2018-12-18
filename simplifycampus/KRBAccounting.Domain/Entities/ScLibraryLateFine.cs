using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
   public class ScLibraryLateFine
    {
       [Key]
        public int Id { get; set; }
       [Required(ErrorMessage = " ")]
       [Remote("LibraryLateFineTitleExists", "Library", AdditionalFields = "Id")]
        public string Title { get; set; }
       [Required(ErrorMessage = " ")]
       //[Remote("LibraryLateFineExists", "Library", AdditionalFields = "Id")]
        public int DayStart { get; set; }
       [Required(ErrorMessage = " ")]
       //[Remote("LibraryLateFineExists", "Library", AdditionalFields = "Id")]
        public int DayEnd { get; set; }
        [Required(ErrorMessage = "Amount must not be empty. ")]
        public decimal Amount { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual User user { get; set; }
    }
}
