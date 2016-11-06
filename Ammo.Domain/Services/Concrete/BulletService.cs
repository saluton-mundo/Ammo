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
    public class BulletService : BaseService, IBulletService
    {
        private IBulletRepository _bulletRepository;

        public BulletService(IBulletRepository bulletRepository)
        {
            _bulletRepository = bulletRepository;
        }

        public int AddUpdate(Bullet Bullet, string SessionUserId)
        {
            if(Bullet == null || string.IsNullOrEmpty(SessionUserId))
            {
                return 0;
            }

            return _bulletRepository.AddUpdate(Bullet, SessionUserId);
        }

        public bool AddUpdate(IEnumerable<Bullet> Bullets, string SessionUserId)
        {
            if (Bullets != null && Bullets.Count() > 0)
            {
                foreach (Bullet bullet in Bullets)
                {
                    AddUpdate(bullet, SessionUserId);
                }

                return true;
            }

            return false;
        }

        public int Delete(int BulletId, string SessionUserId)
        {
            if (BulletId == 0 || string.IsNullOrEmpty(SessionUserId))
            {
                return 0;
            }

            return _bulletRepository.Delete(BulletId, SessionUserId);
        }

        public IEnumerable<Bullet> Get(int? BulletId)
        {
            if(BulletId == 0)
            {
                return null;
            }

            return _bulletRepository.Get(BulletId);
        }
    }
}
