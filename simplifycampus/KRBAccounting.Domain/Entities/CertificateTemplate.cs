using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class Template{        [Key]        public int Id {get;set;}
        [Required(ErrorMessage = "*")]
        [MaxLength]
        public string Description { get; set; }

        public int TemplateType { get; set; }        public int CreatedBy {get;set;}        public DateTime CreatedOn {get;set;}        public int ModifiedBy {get;set;}        public DateTime ModifiedOn {get;set;}

        [NotMapped]
        public int CertificateType { get; set; }

        [NotMapped]
        public string TemplateTypeName { get; set; }    }}