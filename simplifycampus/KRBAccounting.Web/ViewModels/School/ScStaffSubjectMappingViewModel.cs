using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using KRBAccounting.Domain.Entities;
using KRBAccounting.Service.Helpers;

namespace KRBAccounting.Web.ViewModels.School
{
    public class ScStaffSubjectMappingViewModel
    {
        public ScStaffSubjectMapping StaffSubjectMapping { get; set; }
        public IList<CheckBoxListInfo> SubjectList { get; set; }
        public int OldStaffId { get; set; }
       
    }
}