using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Entities
{
    public class ActivityLogEntryMark : BaseEntity
    {
        public int ActivityEntryLogMarkId { get; set; }
        public int ActivityLogId { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }
}
