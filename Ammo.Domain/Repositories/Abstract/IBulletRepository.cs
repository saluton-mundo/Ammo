using Ammo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Repositories.Abstract
{
    public interface IBulletRepository
    {
        IEnumerable<Bullet> Get(int? BulletId);
        int AddUpdate(Bullet Bullet, string SessionUserId);
        int Delete(int BulletId, string SessionUserId);
    }
}
