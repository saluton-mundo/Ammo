using Ammo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Repositories.Abstract
{
    public interface IActivityLogRepository
    {
        // TODO Define CRUD methods to be implemented
        IEnumerable<ActivityLog> Get(int? ActivityLogId, int? JournalId);
        int Delete(int ActivityLogId, string SessionUserId);
        int AddUpdate(ActivityLog Log, string SessionUserId);
    }
}
