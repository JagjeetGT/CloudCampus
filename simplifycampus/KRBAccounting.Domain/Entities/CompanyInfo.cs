using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using KRBAccounting.Domain.Entities;

namespace KRBAccounting.Domain.Entities
{
    public class CompanyInfo
    {
        [Key]
        public int Id { get; set; }
        public string Initial { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }

        [Display(Name = "Pan No")]
        public string PanNo { get; set; }
        [Display(Name = "Tax Invoice")]
        public bool TaxInvoice { get; set; }

        [Display(Name="Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public string StartMiti { get; set; }
        public string EndMiti { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int ParentId { get; set; }
        public string LogoGuid { get; set; }
        public string LogoExt { get; set; }
        [NotMapped]
        public string DisplayDate { get; set; }
        [NotMapped]
        public SystemControl SystemControl { get; set; }

        
        
    }
}
