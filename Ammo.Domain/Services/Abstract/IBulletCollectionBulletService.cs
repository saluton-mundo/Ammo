using Ammo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Services.Abstract
{
    public interface IBulletCollectionBulletService
    {
        IEnumerable<Bullet> Get(int BulletCollectionId);
        bool AddUpdate(int BulletCollectionId, IEnumerable<Bullet> Bullets, string SessionUserId);
        int AddUpdate(int BulletCollectionId, Bullet Bullet, string SessionUserId);
        int Delete(int BulletCollectionId, int BulletId, string SessionUserId);
    }
}
