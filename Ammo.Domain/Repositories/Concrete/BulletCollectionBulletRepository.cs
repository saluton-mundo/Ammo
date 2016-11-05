using Ammo.Domain.Entities;
using Ammo.Domain.Repositories.Abstract;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Repositories.Concrete
{
    public class BulletCollectionBulletRepository : BaseRepository, IBulletCollectionBulletRepository
    {
        public IEnumerable<Bullet> Get(int BulletCollectionId)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.Query<Bullet>("spBulletCollectionBulletGet",
                                                                new
                                                                {
                                                                    @BULLETCOLLECTIONID = BulletCollectionId
                                                                },
                                                                null, 
                                                                true, 
                                                                null, 
                                                                System.Data.CommandType.StoredProcedure)).Result;
            }
        }

        public bool AddUpdate(int BulletCollectionId, IEnumerable<Bullet> Bullets, string SessionUserId)
        {
            if(Bullets != null && Bullets.Count() > 0)
            {
                foreach(Bullet bullet in Bullets)
                {
                    AddUpdate(BulletCollectionId, bullet, SessionUserId);
                }
                // We carried out DB operations so return true
                return true;
            }
            // Nothing to carry out so return false
            return false;
        }

        public int AddUpdate(int BulletCollectionId, Bullet Bullet, string SessionUserId)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.ExecuteScalar<int>("spBulletCollectionBulletAddUpdate",
                                                                     new
                                                                     {
                                                                         @BULLETCOLLECTIONID = BulletCollectionId,
                                                                         @BULLETID = Bullet.BulletId,
                                                                         @SESSIONUSERID = Guid.Parse(SessionUserId)
                                                                     }, 
                                                                     null, 
                                                                     null, 
                                                                     System.Data.CommandType.StoredProcedure)).Result;
            }
        }

        public int Delete(int BulletCollectionId, int BulletId, string SessionUserId)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.ExecuteScalar<int>("spBulletCollectionBulletDelete",
                                                                     new
                                                                     {
                                                                         @BULLETCOLLECTIONID = BulletCollectionId,
                                                                         @BULLETID = BulletId,
                                                                         @SESSIONUSERID = Guid.Parse(SessionUserId)
                                                                     },
                                                                     null,
                                                                     null,
                                                                     System.Data.CommandType.StoredProcedure)).Result;
            }
        }
    }
}
