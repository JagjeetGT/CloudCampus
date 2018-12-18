using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Remote("CheckRoleName", "User", AdditionalFields = "Id")]
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
