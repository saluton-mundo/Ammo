using Ammo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Repositories.Abstract
{
    public interface IJournalBulletCollectionRepository
    {
        JournalBulletCollection Get(int JournalId);

        int AddUpdate(int JournalId, int BulletCollectionId, string SessionUserId);

        int Delete(int JournalId, int BulletCollectionId, string SessionUserId);
    }
}
