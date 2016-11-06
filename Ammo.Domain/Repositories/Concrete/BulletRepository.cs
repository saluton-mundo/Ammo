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
    public class BulletRepository : BaseRepository, IBulletRepository
    {
        public IEnumerable<Bullet> Get(int? BulletId)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.Query<Bullet>("spBulletGet", 
                                                                new
                                                                {
                                                                    @BULLETID = BulletId
                                                                }, 
                                                                null, 
                                                                true, 
                                                                null,
                                                                System.Data.CommandType.StoredProcedure)).Result;
            }
        }

        public IEnumerable<Bullet> GetByCollection(int BulletCollectionId)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.Query<Bullet>("spBulletGetByCollection",
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

        public int AddUpdate(Bullet Bullet, string SessionUserId)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.ExecuteScalar<int>("spBulletAddUpdate",
                                                                     new
                                                                     {
                                                                         @BULLETID = Bullet.BulletId,
                                                                         @NAME  = Bullet.Name,
                                                                         @IMAGEURI = Bullet.ImageUri,
                                                                         @SESSIONUSERID = Guid.Parse(SessionUserId)
                                                                     },
                                                                     null,
                                                                     null,
                                                                     System.Data.CommandType.StoredProcedure)).Result;
            }
        }

        public int Delete(int BulletId, string SessionUserId)
        {
            using (var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.ExecuteScalar<int>("spBulletDelete",
                                                                     new
                                                                     {
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
