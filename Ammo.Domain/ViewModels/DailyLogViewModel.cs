using Ammo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.ViewModels
{
    public class DailyLogViewModel : BaseViewModel
    {
        public int JournalPageId { get; set; }
        public DateTime DailyLogDate { get; set; }
        public IEnumerable<DailyLogBullet> Bullets { get; set; }
    }
}
