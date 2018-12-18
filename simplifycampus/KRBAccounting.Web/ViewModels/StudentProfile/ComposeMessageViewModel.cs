using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Web.ViewModels.StudentProfile
{
    public class ComposeMessageViewModel
    {
        public IEnumerable<UserList> UserLists { get; set; }
        public ScMessage Messages { get; set; }
    }
    public class UserList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
    }
}