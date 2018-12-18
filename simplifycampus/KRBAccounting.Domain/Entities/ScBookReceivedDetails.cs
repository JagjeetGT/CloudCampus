using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScBookReceivedDetails
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public int Status { get; set; }
        public string BarCode { get; set; }
        [Required(ErrorMessage = " ")]
        public string AccessionNo { get; set; }
        public string Remarks { get; set; }
        public int MasterId { get; set; }

     

        [NotMapped]
        public string BookName { get; set; }
        [NotMapped]
        public string MFNNo { get; set; }

      
        [ForeignKey("MasterId")]
        public virtual ScBookReceived BookReceived { get; set; }
    }

}
