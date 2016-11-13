using Ammo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Repositories.Abstract
{
    public interface IActivityLogActivityRepository
    {
        IEnumerable<ActivityLogActivity> Get(int ActivityLogId, int? ActivityLogActivityId);
        int AddUpdate(ActivityLogActivity Activity, string SessionUserId);
        int Delete(int ActivityLogActivityId, string SessionUserId);
    }
}
