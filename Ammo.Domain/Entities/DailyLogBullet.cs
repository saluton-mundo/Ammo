using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Entities
{
    public class DailyLogBullet : Bullet
    {
        public DateTime DailyLogDate { get; set; }
    }
}
