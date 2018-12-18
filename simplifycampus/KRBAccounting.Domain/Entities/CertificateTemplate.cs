using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{    public class Template
        [Required(ErrorMessage = "*")]
        [MaxLength]
        public string Description { get; set; }

        public int TemplateType { get; set; }

        [NotMapped]
        public int CertificateType { get; set; }

        [NotMapped]
        public string TemplateTypeName { get; set; }