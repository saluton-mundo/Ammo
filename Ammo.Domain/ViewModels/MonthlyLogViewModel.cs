using Ammo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.ViewModels
{
    public class MonthlyLogViewModel : BaseViewModel
    {
        public int JournalPageId { get; set; }
        public DateTime MonthlyLogMonth { get; set; }
        public IEnumerable<MonthlyLogBullet> Bullets { get; set; }
    }
}
