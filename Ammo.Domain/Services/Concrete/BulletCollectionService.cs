using Ammo.Domain.Entities;
using Ammo.Domain.Repositories.Abstract;
using Ammo.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Services.Concrete
{
    public class BulletCollectionService : BaseService, IBulletCollectionService
    {
        private IBulletCollectionRepository _collectionRepository;

        public BulletCollectionService(IBulletCollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository;
        }

        public BulletCollection Get(int BulletCollectionId)
        {
            // TODO: Call config layer and get Default Collection Id...
            if(BulletCollectionId == 0)
            {
                BulletCollectionId = 2;
            }

            return _collectionRepository.Get(BulletCollectionId);
        }

        public IEnumerable<BulletCollection> GetAll()
        {
            return _collectionRepository.GetAll();
        }

        public IEnumerable<BulletCollection> GetByUser(string UserName)
        {
            return _collectionRepository.GetByUser(UserName);
        }

        public int AddUpdate(BulletCollection Collection, string SessionUser)
        {
            return _collectionRepository.AddUpdate(Collection, SessionUser);
        }

        public int Delete(int BulletCollectionId, string SessionUser)
        {
            return _collectionRepository.Delete(BulletCollectionId, SessionUser);
        }
    } 
}
