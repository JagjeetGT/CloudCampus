using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{
    public class ProductSubGroup
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        [Remote("CheckDescriptionInProductSubGroup", "Master", AdditionalFields = "ProductGroupId,Id")]
        public string Description { get; set; }
        [Required(ErrorMessage = " ")]
        public int ProductGroupId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedById { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ProductGroupId")]
        public virtual ProductGroup ProductGroup { get; set; }
        //,ProductGroupId
    }
}
