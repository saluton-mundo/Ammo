﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Entities
{
    public class BulletCollection : BaseEntity
    {
        public int BulletCollectionId { get; set; }
        public string Name { get; set; }
        public Guid CreateUserId { get; set; }
        public bool IsAmmoDefault { get; set; }
        public IEnumerable<Bullet> Bullets { get; set; }
    }
}
