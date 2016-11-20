using Ammo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.ViewModels
{
    public class ActivityLogEntryViewModel
    {
        public DateTime Day { get; set; }
        public ActivityLogEntry Entry { get; set; }
    }
}
