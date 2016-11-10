using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Entities
{
    public class ActivityLog : BaseEntity
    {
        // Who owns this activity log
        public string OwnerId { get; set; }
        // Which journal does this log belong to
        public int JournalId { get; set; }
        // Which page contains the activity log
        public int JournalPageNo { get; set; }
        // Where on the page should the activity log appear?
        public int JournalPageSortOrder { get; set; }
        // Activities tracks in this log
        public IEnumerable<ActivityLogActivity> Activities { get; set; }
        // Which month does this log represent
        public DateTime Month{ get; set; }
    }
}
