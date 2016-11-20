using Ammo.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ammo.Domain.Entities;
using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace Ammo.Domain.Repositories.Concrete
{
    public class ActivityLogEntryRepository : BaseRepository, IActivityLogEntryRepository
    {
        public int AddUpdate(ActivityLogEntry Entry, string SessionUserId)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.ExecuteScalar<int>("spActivityLogEntryAddUpdate",
                                                                     new
                                                                     {
                                                                         @ACTIVITYLOGENTRYID = Entry.ActivityLogEntryId,
                                                                         @ACTIVITYLOGID = Entry.ActivityLogId,
                                                                         @ACTIVITYID = Entry.ActivityId,
                                                                         @ACTIVITYLOGENTRYMARKID = Entry.ActivityLogEntryMarkId,
                                                                         @ENTRYDATE = Entry.EntryDate,
                                                                         @SESSIONUSERID = SessionUserId
                                                                     },
                                                                     null,
                                                                     null,
                                                                     CommandType.StoredProcedure)).Result;
            }
        }

        public int Delete(int ActivityLogEntryId, string SessionUserId)
        {
            using (var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.ExecuteScalar<int>("spActivityLogEntryDelete", 
                                                                     new
                                                                     {
                                                                        @ACTIVITYLOGENTRYID = ActivityLogEntryId,
                                                                        @SESSIONUSERID = SessionUserId
                                                                     }, 
                                                                     null,
                                                                     null, 
                                                                     CommandType.StoredProcedure)).Result;
            }
        }

        public IEnumerable<ActivityLogEntry> Get(int? ActivityLogEntryId, int? ActivityLogId, int? ActivityId)
        {
            using (var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.Query<ActivityLogEntry>("spActivityLogEntryGet",
                                                                          new
                                                                          {
                                                                              @ACTIVITYLOGENTRYID = ActivityLogEntryId,
                                                                              @ACTIVITYLOGID = ActivityLogId,
                                                                              @ACTIVITYID = ActivityId
                                                                          },
                                                                          null,
                                                                          true,
                                                                          null,
                                                                          CommandType.StoredProcedure)).Result;
            }
        }
    }
}
