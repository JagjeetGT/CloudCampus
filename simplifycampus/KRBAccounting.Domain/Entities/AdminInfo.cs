using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class AdminInfo
    {
        [Required]
        public string AdminUserName { get; set; }
        [Required]
        public string AdminUserPassword { get; set; }
    }
}
