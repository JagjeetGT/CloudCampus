using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Service;

namespace KRBAccounting.Web.ViewModels.Teacher
{
    public class TeacherSubjectViewModel
    {
    
        public List<UtilityService.Classes> Classes { get; set; }
        public ScSubject Subject { get; set; }
        public int TeacherId { get; set; }
       
    }
}