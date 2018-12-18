using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class DatabaseInfo
    {
        [Required]
        public string ServerName { get; set; }
        
        public string DbUserName { get; set; }
       
        public string DbPassword { get; set; }
    }
}
