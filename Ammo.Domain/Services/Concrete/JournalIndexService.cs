using Ammo.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ammo.Domain.Entities;
using Ammo.Domain.Repositories.Abstract;

namespace Ammo.Domain.Services.Concrete
{
    public class JournalIndexService : BaseService, IJournalIndexService
    {
        IJournalIndexRepository _indexRepository;
        IBulletRepository _bulletRepository;

        public JournalIndexService(IBulletRepository bulletRepository,
                                   IJournalIndexRepository indexRepository)
        {
            _bulletRepository = bulletRepository;
            _indexRepository = indexRepository;
        }

        public JournalIndex Get(int JournalId, string SessionUserId)
        {
            JournalIndex index = _indexRepository.Get(JournalId);

            index.BulletCollection.Bullets = _bulletRepository.GetByCollection(index.BulletCollection.BulletCollectionId);

            if(index.OwnerId.ToString().ToUpper() == SessionUserId.ToUpper())
            {
                return index;
            }

            return null;
        }
    }
}
