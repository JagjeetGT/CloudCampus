using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace KRBAccounting.Domain.Entities
{
    public class CheckPoints
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public string IpAddress { get; set; }
        public string ComputerName { get; set; }
    }
}
