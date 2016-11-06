using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Entities
{
    public class BulletCollectionBullet : BaseEntity
    {
        public int BulletCollectionId { get; set; }

        public Bullet Bullet { get; set; }
    }
}
