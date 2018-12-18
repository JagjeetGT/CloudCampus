using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KRBAccounting.Domain.Entities
{
    public class QueuedJob
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public DateTime LastRunDate { get; set; }
        public DateTime ScheduleDate { get; set; }

    }
}
