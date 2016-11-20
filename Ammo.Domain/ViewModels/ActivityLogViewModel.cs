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

        public IEnumerable<ActivityLogEntryViewModel> EntryViewModel
        {
            get
            {
                List<ActivityLogEntryViewModel> list = new List<ActivityLogEntryViewModel>();
                foreach(DateTime day in ActivityLog.LogDays)
                {
                    ActivityLogEntryViewModel model = new ActivityLogEntryViewModel()
                    {
                        Day = day,
                        Entry = Entries.Where(e => e.EntryDate.Date == day.Date).FirstOrDefault()
                    };

                    list.Add(model);
                }

                return list;
            }
        }

        public IEnumerable<ActivityLogEntry> Entries { get; set; }
        public IEnumerable<ActivityLogEntryMark> Marks { get; set; }
    }
}
