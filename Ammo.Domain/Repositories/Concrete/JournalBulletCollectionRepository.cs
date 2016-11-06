using Ammo.Domain.Repositories.Abstract;
using Dapper;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ammo.Domain.Entities;
using System.Data.SqlClient;
using static Dapper.SqlMapper;

namespace Ammo.Domain.Repositories.Concrete
{
    public class JournalBulletCollectionRepository : BaseRepository, IJournalBulletCollectionRepository
    {
        public int AddUpdate(int JournalId, int BulletCollectionId, string SessionUserId)
        {
            using (var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.ExecuteScalar<int>("spJournalBulletCollectionAddUpdate",
                                                                     new
                                                                     {
                                                                         @BULLETCOLLECTIONID = BulletCollectionId,
                                                                         @JOURNALID = JournalId,
                                                                         @SESSIONUSERID = Guid.Parse(SessionUserId)
                                                                     },
                                                                     null,
                                                                     null,
                                                                     CommandType.StoredProcedure)).Result;
            }
        }

        public int Delete(int JournalId, int BulletCollectionId, string SessionUserId)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.ExecuteScalar<int>("spJournalBulletCollectionDelete", 
                                                                     new
                                                                     {
                                                                         @BULLETCOLLECTIONID = BulletCollectionId,
                                                                         @JOURNALID = JournalId,
                                                                         @SESSIONUSERID = Guid.Parse(SessionUserId)
                                                                     }, 
                                                                     null, 
                                                                     null, 
                                                                     CommandType.StoredProcedure)).Result;
            }
        }

        public JournalBulletCollection Get(int JournalId)
        {
            JournalBulletCollection collection = new JournalBulletCollection();

            using(var connection = new SqlConnection(base.ConnectionString))
            {
                using (GridReader reader = connection.QueryMultiple("spJournalBulletCollectionGet", new { @JOURNALID = JournalId }, null, null, CommandType.StoredProcedure))
                {
                    collection = reader.Read<JournalBulletCollection>().SingleOrDefault();

                    collection.Collections = reader.Read<BulletCollection>().ToList();
                }
            }

            return collection;
        }
    }
}
