using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class ScBus
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " ")]
        public string Description { get; set; }
        [Required(ErrorMessage = " ")]
        public string VehicleNo { get; set; }

        public int MaxCapicity { get; set; }
        public string DriverName { get; set; }
        public string DriverLicense { get; set; }
        public string HelperName { get; set; }

        public string ContactOff { get; set; }
        public string ContactPhNo { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonPhNo { get; set; }
        public string FeeMemo { get; set; }
    }
}
