using Ammo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.ViewModels
{
    public class ActivityLogViewModel : BaseViewModel
    {
        public ActivityLog ActivityLog { get; set; }
        public IEnumerable<ActivityLogEntry> Entries { get; set; }
        public IEnumerable<ActivityLogEntryMark> Marks { get; set; }
    }
}
