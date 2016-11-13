using Ammo.Domain.Repositories.Abstract;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ammo.Domain.Entities;
using System.Data.SqlClient;

namespace Ammo.Domain.Repositories.Concrete
{
    public class ActivityLogRepository : BaseRepository, IActivityLogRepository
    {
        public int AddUpdate(ActivityLog Log, string SessionUserId)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.ExecuteScalar<int>("spActivityLogAddUpdate",
                                                                     new
                                                                     {
                                                                         @ACTIVITYLOGID = Log.ActivityLogId,
                                                                         @JOURNALID = Log.JournalId,
                                                                         @OWNERID = Guid.Parse(Log.OwnerId),
                                                                         @ACTIVITYLOGMONTH = Log.LogMonth,
                                                                         @SESSIONUSERID = Guid.Parse(SessionUserId)
                                                                     },
                                                                     null,
                                                                     null,
                                                                     CommandType.StoredProcedure)).Result;
            }
        }

        public int Delete(int ActivityLogId, string SessionUserId)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.ExecuteScalar<int>("spActivityLogDelete",
                                                                     new
                                                                     {
                                                                         @ACTIVITYLOGID = ActivityLogId,
                                                                         @SESSIONUSERID = Guid.Parse(SessionUserId)
                                                                     },
                                                                     null,
                                                                     null,
                                                                     CommandType.StoredProcedure)).Result;
            }
        }

        public IEnumerable<ActivityLog> Get(int? ActivityLogId, int? JournalId)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.Query<ActivityLog>("spActivityLogGet", 
                                                                     new
                                                                     {
                                                                         @ACTIVITYLOGID = ActivityLogId,
                                                                         @JOURNALID = JournalId
                                                                     }, 
                                                                     null, 
                                                                     true, 
                                                                     null, 
                                                                     CommandType.StoredProcedure)).Result;
            }
        }
    }
}
