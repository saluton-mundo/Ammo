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
            throw new NotImplementedException();
        }

        public int Delete(int ActivityLogId, string SessionUserId)
        {
            throw new NotImplementedException();
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
