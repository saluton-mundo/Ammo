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
    public class JournalBulletCollectionService : BaseService, IJournalBulletCollectionService
    {
        private IJournalBulletCollectionRepository _journalBulletCollectionRepository;

        public JournalBulletCollectionService(IJournalBulletCollectionRepository journalBulletCollectionRepository)
        {
            _journalBulletCollectionRepository = journalBulletCollectionRepository;
        }

        public int AddUpdate(int JournalId, int BulletCollectionId, string SessionUserId)
        {
            if (JournalId == 0 || BulletCollectionId == 0 || string.IsNullOrEmpty(SessionUserId))
            {
                return 0;
            }

            return _journalBulletCollectionRepository.AddUpdate(JournalId, BulletCollectionId, SessionUserId);
        }

        public int Delete(int JournalId, int BulletCollectionId, string SessionUserId)
        {
            if(JournalId == 0 || BulletCollectionId == 0 || string.IsNullOrEmpty(SessionUserId))
            {
                return 0;
            }

            return _journalBulletCollectionRepository.Delete(JournalId, BulletCollectionId, SessionUserId);
        }

        public JournalBulletCollection Get(int JournalId)
        {
            if(JournalId == 0)
            {
                return null;
            }

            return _journalBulletCollectionRepository.Get(JournalId);
        }
    }
}
