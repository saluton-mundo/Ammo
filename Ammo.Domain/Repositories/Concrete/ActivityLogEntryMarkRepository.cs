using Ammo.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Ammo.Domain.Entities;
using Dapper;

namespace Ammo.Domain.Repositories.Concrete
{
    public class ActivityLogEntryMarkRepository : BaseRepository, IActivityLogEntryMarkRepository
    {
        public int AddUpdate(ActivityLogEntryMark EntryMark, string SessionUserId)
        {
            using(SqlConnection connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.ExecuteScalar<int>("spActivityLogEntryMarkAddUpdate",
                                                                     new
                                                                     {
                                                                         @ACTIVITYLOGENTRYMARKID = EntryMark.ActivityEntryLogMarkId,
                                                                         @ACTIVITYLOGID = EntryMark.ActivityLogId,
                                                                         @DESCRIPTION = EntryMark.Description,
                                                                         @COLOR = EntryMark.Color,
                                                                         @SESSIONUSERID = Guid.Parse(SessionUserId)
                                                                     },
                                                                     null,
                                                                     null,
                                                                     CommandType.StoredProcedure)).Result;
            }
         }

        public int Delete(int ActivityLogEntryMarkId, string SessionUserId)
        {
            using (SqlConnection connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.ExecuteScalar<int>("spActivityLogEntryMarkDelete",
                                                                     new
                                                                     {
                                                                         @ACTIVITYLOGENTRYMARKID = ActivityLogEntryMarkId,
                                                                         @SESSIONUSERID = Guid.Parse(SessionUserId)
                                                                     },
                                                                     null,
                                                                     null,
                                                                     CommandType.StoredProcedure)).Result;
            }
        }

        public IEnumerable<ActivityLogEntryMark> Get(int? ActivityLogId, int? ActivityLogEntryMarkId)
        {
            using (SqlConnection connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.Query<ActivityLogEntryMark>("spActivityLogEntryMarkGet",
                                                                               new
                                                                               {
                                                                                    @ACTIVITYLOGID = ActivityLogId,
                                                                                    @ACTIVITYLOGENTRYMARKID = ActivityLogEntryMarkId
                                                                               },
                                                                               null,
                                                                               true,
                                                                               null,
                                                                               CommandType.StoredProcedure)).Result;
            }
        }
    }
}
