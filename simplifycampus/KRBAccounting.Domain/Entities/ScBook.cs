using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScBook
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string VolumeNumber { get; set; }
        public string RackNo { get; set; }
        public string BookNumber { get; set; }
        public string Language { get; set; }
        public string MFNNumber { get; set; }
        public string Keywords { get; set; }
        public DateTime YearOfPublication { get; set; }
        public string MitiOfPublication { get; set; }
        public string Edition { get; set; }
        public string ImprintPlace { get; set; }
        public string Publisher { get; set; }
        public string CorporateBody { get; set; }
        public string PersonalAuthor { get; set; }
        public string Pages { get; set; }
        public string Series { get; set; }
        public string BoardSubHeading { get; set; }
        public string ISBN { get; set; }
        public string ISSN { get; set; }
        public string Source { get; set; }
        public string LibraryLocation { get; set; }
        public string TypeOfMaterial { get; set; }
        [Required(ErrorMessage = " ")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = " ")]
        public string RowNo { get; set; }
        public bool IsIssuable { get; set; }
        public string Notes { get; set; }
        public int CreateById { get; set; }
        public int UpdateById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        [Required]
        public string BarCode { get; set; }
        [NotMapped]
        public string DisplayYear { get; set; }

        [NotMapped]
        public string DisplayYearPublication { get; set; }

        [NotMapped]
        public SystemControl SystemControl { get; set; }

        [ForeignKey("CreateById")]
        public virtual User Create { get; set; }

        [ForeignKey("UpdateById")]
        public virtual User Update { get; set; }
    }
}
