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
    public class BulletCollectionBulletService : BaseService, IBulletCollectionBulletService
    {
        private IBulletCollectionBulletRepository _collectionBulletRepository;

        public BulletCollectionBulletService(IBulletCollectionBulletRepository collectionBulletRepository)
        {
            _collectionBulletRepository = collectionBulletRepository;
        }

        public int AddUpdate(int BulletCollectionId, Bullet Bullet, string SessionUserId)
        {
            if(BulletCollectionId == 0 || Bullet == null || string.IsNullOrEmpty(SessionUserId))
            {
                return 0;
            }

            return _collectionBulletRepository.AddUpdate(BulletCollectionId, Bullet, SessionUserId);
        }

        public bool AddUpdate(int BulletCollectionId, IEnumerable<Bullet> Bullets, string SessionUserId)
        {
            if (BulletCollectionId == 0 || Bullets == null || string.IsNullOrEmpty(SessionUserId))
            {
                return false;
            }

            return _collectionBulletRepository.AddUpdate(BulletCollectionId, Bullets, SessionUserId);
        }

        public int Delete(int BulletCollectionId, int BulletId, string SessionUserId)
        {
            if(BulletCollectionId == 0 || BulletId == 0 || string.IsNullOrEmpty(SessionUserId))
            {
                return 0;
            }

            return _collectionBulletRepository.Delete(BulletCollectionId, BulletId, SessionUserId);
        }

        public IEnumerable<Bullet> Get(int BulletCollectionId)
        {
            if(BulletCollectionId == 0)
            {
                return null;
            }

            return _collectionBulletRepository.Get(BulletCollectionId);
        }
    }
}
