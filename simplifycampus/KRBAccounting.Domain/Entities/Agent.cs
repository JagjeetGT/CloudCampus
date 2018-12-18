using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class Agent
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required Field Missing!")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Please Insert Alphabet Only.")]
        [Remote("CheckNameInAgent", "Master", AdditionalFields = "Id")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required Field Missing!")]
        [RegularExpression(@"^[\w ]+$", ErrorMessage = "Please Insert AlphaNumeric Only.")]
        [Remote("CheckShortNameInAgent", "Master", AdditionalFields = "Id")]
        public string ShorName { get; set; }

        [RegularExpression(@"^[\w, ]+$", ErrorMessage = "Please Insert AlphaNumeric and (,) Only.")]
        public string Address { get; set; }

        //[RegularExpression(@"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$", ErrorMessage = "Fill Phone No. In Correct Fromat.")]
        [RegularExpression(@"^[+]?[\d- ]+$", ErrorMessage="Invalid phone number.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNo { get; set; }

        //[RegularExpression(@"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$", ErrorMessage = "Fill Fax No. In Correct Fromat.")]
        [RegularExpression(@"^[+]?[\d- ]+$", ErrorMessage = "Invalid Fax number.")]
        [DataType(DataType.PhoneNumber)]
        public string Fax { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
        ErrorMessage = "Please enter a valid e-mail adress!! E.g. abc@company.com.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

       // [Required(ErrorMessage="Required Field Missing!")]
        public int? LedgerId { get; set; }
        public int? SubLedgerId { get; set; }
        public decimal? Commision { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedById { get; set; }

        public int Area { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int BranchId { get; set; }

        [ForeignKey("BranchId")]
        public CompanyInfo Branch { get; set; }
    }
}
