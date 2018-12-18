using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KRBAccounting.Domain.Entities
{    public class SmsGroup
        [Remote("CheckSmsGroup", "Sms", AdditionalFields = "Id")]

        [NotMapped]
        public SelectList GroupList { get; set; }

        [NotMapped]
        public int SmsGroupTypeId { get; set; }

        [NotMapped]
        public int ClassId { get; set; }

        [NotMapped]
        public SelectList Classlist { get; set; }

        [NotMapped]
        public SelectList Stafflist { get; set; }

        [NotMapped]
        public IEnumerable<SmsGroupDetail> SmsGroupDetailList { get; set; }