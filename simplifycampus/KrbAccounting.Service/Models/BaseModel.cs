using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Service.Models
{
    public class BaseModel
    {
        public SystemControl SystemControl { get; set; }
        public CompanyInfo CompanyInfo { get; set; }
        public FiscalYear FiscalYear { get; set; }
        public UserRight UserRight { get; set; }
    }
    public class UserRight
    {
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Navigate { get; set; }
        public bool Approve { get; set; }
    }
}
