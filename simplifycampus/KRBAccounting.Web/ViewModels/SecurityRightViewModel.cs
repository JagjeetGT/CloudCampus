using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KRBAccounting.Web.ViewModels
{
    public class SecurityRightViewModel
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public int Role { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Navigate { get; set; }
        public bool Approve { get; set; }
       
        
        public int? MenuItemId { get; set; }

        
        public int? ParentId { get; set; }

     
        public string LinkName { get; set; }
    }
}