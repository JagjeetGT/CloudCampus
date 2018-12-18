using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Service.Models.StudentProfile
{
    public class NotificationDashboard:ScMessage
    {
        public string SenderName { get; set; }
       
    }
}