using Ammo.Domain.Repositories.Abstract;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ammo.Domain.Entities;

namespace Ammo.Domain.Repositories.Concrete
{
    public class ActivityLogActivityRepository : BaseRepository, IActivityLogActivityRepository
    {
        public int AddUpdate(ActivityLogActivity Activity, string SessionUserId)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.ExecuteScalar<int>("spActivityLogActivityAddUpdate", 
                                                                     new
                                                                     {
                                                                         @ACTIVITYLOGACTIVITYID = Activity.ActivityId,
                                                                         @ACTIVITYLOGID = Activity.ActivityLogId,
                                                                         @DESCRIPTION = Activity.Description,
                                                                         @SESSIONUSERID = Guid.Parse(SessionUserId)
                                                                     },
                                                                     null,
                                                                     null,
                                                                     CommandType.StoredProcedure)).Result;
            }
        }

        public int Delete(int ActivityLogActivityId, string SessionUserId)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.ExecuteScalar<int>("spActivityLogActivityDelete",
                                                                     new
                                                                     {
                                                                         @ACTIVITYLOGACTIVITYID = ActivityLogActivityId,
                                                                         @SESSIONUSERID = Guid.Parse(SessionUserId)
                                                                     },
                                                                     null,
                                                                     null,
                                                                     CommandType.StoredProcedure)).Result;
            }
        }

        public IEnumerable<ActivityLogActivity> Get(int ActivityLogId, int? ActivityLogActivityId)
        {
            using(var connection = new SqlConnection(base.ConnectionString))
            {
                return Task.FromResult(connection.Query<ActivityLogActivity>("spActivityLogActivityGet", 
                                                                             new
                                                                             {
                                                                                 @ACTIVITYLOGID = ActivityLogId,
                                                                                 @ACTIVITYLOGACTIVITYID = ActivityLogActivityId
                                                                             },
                                                                             null,
                                                                             true,
                                                                             null,
                                                                             CommandType.StoredProcedure)).Result;
            }
        }
    }
}
