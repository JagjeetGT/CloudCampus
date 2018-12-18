using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace KRBAccounting.Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "User Name")]
        [MaxLength(100)]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed")]
        public string Username { get; set; }


        [Display(Name = "First Name")]
        [MaxLength(100)]
        public string FullName { get; set; }


        //[Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(200)]
        public string EmailAddress { get; set; }

        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string LastLoginIp { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CompanyId { get; set; }
        public int? BranchId { get; set; }
        public bool AllBranch { get; set; }
        public int LastAccessedBranch { get; set; }
        public string ImageGuid { get; set; }
        public string Ext { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        //[ForeignKey("CompanyId")]
        //public virtual CompanyInfo CompanyInfo { get; set; }
    }
}
