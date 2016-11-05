using Ammo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Services.Abstract
{
    public interface IBulletCollectionService
    {
        int AddUpdate(BulletCollection Collection, string SessionUser);
        int Delete(int BulletCollectionId, string SessionUser);
        BulletCollection Get(int BulletCollectionId);
        IEnumerable<BulletCollection> GetByUser(string SessionUser);
        IEnumerable<BulletCollection> GetAll();
    }
}
