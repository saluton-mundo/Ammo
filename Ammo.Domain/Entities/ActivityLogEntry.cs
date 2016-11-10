using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Entities
{
    public class ActivityLogEntry : BaseEntity
    {
        public int ActivityLogEntryId { get; set; }
        public int ActivityLogId { get; set; }
        public int ActivityId { get; set; }
        public DateTime ActivityLogEntryDate { get; set; }
        public ActivityLogEntryMark Mark { get; set; }
    }
}
