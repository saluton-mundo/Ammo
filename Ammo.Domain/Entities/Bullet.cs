using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Entities
{
    public class Bullet : BaseEntity
    {
        public int BulletId { get; set; }
        public string Name { get; set; }
        public string ImageUri { get; set; }
        public int CreatorId { get; set; }
    }
}
