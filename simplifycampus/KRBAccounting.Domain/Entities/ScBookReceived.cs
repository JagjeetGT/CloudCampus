using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Domain.Entities
{
    public class ScBookReceived
    {
     

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public string Number { get; set; }
        [Required(ErrorMessage = " ")]
        public DateTime Date { get; set; }

        public string Miti { get; set; }
        public int BookId { get; set; }
        [Required(ErrorMessage = " ")]
        public int Quantity { get; set; }
        public string Remarks { get; set; }
        public int CreatedById { get; set; }
        public int UpdatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        [NotMapped]
        public IEnumerable<ScBookReceivedDetails> ReceivedDetailses { get; set; }
            [ForeignKey("BookId")]
        public virtual ScBook Book { get; set; }

            [NotMapped]
            public string DisplayDate { get; set; }

            [NotMapped]
            public SystemControl SystemControl { get; set; }

    }
}
