using Ammo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Repositories.Abstract
{
    public interface IBulletCollectionRepository
    {
        BulletCollection Get(int BulletCollectionId);
        int Delete(int BulletCollectionId, string SessionUser);
        IEnumerable<BulletCollection> GetAll();
        IEnumerable<BulletCollection> GetByUser(string UserName);
        int AddUpdate(BulletCollection Collection, string SessionUser);
    }
}
