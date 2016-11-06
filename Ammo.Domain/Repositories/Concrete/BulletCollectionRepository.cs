using Ammo.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ammo.Domain.Entities;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using static Dapper.SqlMapper;
using Ammo.Domain.Extensions.ORM;

namespace Ammo.Domain.Repositories.Concrete
{
    public class BulletCollectionRepository : BaseRepository, IBulletCollectionRepository
    {
        public BulletCollection Get(int BulletCollectionId)
        {
            using (var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.Query<BulletCollection>("spBulletCollectionGet",
                                                                 new
                                                                 {
                                                                     @BULLETCOLLECTIONID = BulletCollectionId,
                                                                     @SESSIONUSERID = (int?)null
                                                                 },
                                                                 null,
                                                                 true,
                                                                 null,
                                                                 CommandType.StoredProcedure))
                                                  .Result
                                                  .FirstOrDefault();
            }
        }

        public IEnumerable<BulletCollection> GetAll()
        {
            using (var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.Query<BulletCollection>("spBulletCollectionGet",
                                                                 new
                                                                 {
                                                                     @BULLETCOLLECTIONID = (int?)null,
                                                                     @SESSIONUSERID = (int?)null
                                                                 },
                                                                 null,
                                                                 true,
                                                                 null,
                                                                 CommandType.StoredProcedure)).Result;
            }
        }

        public IEnumerable<BulletCollection> GetByUser(string UserName)
        {
            using (var connection = new SqlConnection(base.ConnectionString))
            {
                using (GridReader reader = connection.QueryMultiple("spBulletCollectionGet",
                                                                    new
                                                                    {
                                                                        @BULLETCOLLECTIONID = (int?)null,
                                                                        @SESSIONUSERID = Guid.Parse(UserName)
                                                                    },
                                                                    null,
                                                                    null,
                                                                    CommandType.StoredProcedure))
                {
                    List<BulletCollection> collections = reader.Read<BulletCollection>().ToList();
                    IEnumerable<Bullet> bullets = reader.Read<Bullet>();

                    if (collections != null && collections.Count > 0)
                    {
                        foreach(BulletCollection collection in collections)
                        {
                            collection.Bullets = bullets.Where(b => b.BulletCollectionId == collection.BulletCollectionId)
                                                        .Distinct()
                                                        .ToList();
                        }
                    }

                    return collections;
                }
            }
        }

        public int AddUpdate(BulletCollection Collection, string SessionUser)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.ExecuteScalar<int>("spBulletCollectionAddUpdate",
                                                                     new
                                                                     {
                                                                         @BULLETCOLLECTIONID = Collection.BulletCollectionId,
                                                                         @NAME = Collection.Name,
                                                                         @ISAMMODEFAULT = Collection.IsAmmoDefault,
                                                                         @SESSIONUSERID = SessionUser
                                                                     },
                                                                     null,
                                                                     null,
                                                                     CommandType.StoredProcedure)).Result;

            }
        }

        public int Delete(int BulletCollectionId, string SessionUser)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.ExecuteScalar<int>("spBulletCollectionDelete",
                                                                     new
                                                                     {
                                                                         @BULLETCOLLECTIONID = BulletCollectionId
                                                                         ,@SESSIONUSERID = Guid.Parse(SessionUser)
                                                                     },
                                                                     null,
                                                                     null,
                                                                     CommandType.StoredProcedure)).Result;
            }
        }
    }
}
