using Ammo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Services.Abstract
{
    public interface IActivityLogEntryMarkService
    {
        IEnumerable<ActivityLogEntryMark> Get(int? ActivityLogId, int? ActivityLogEntryMarkId);
        int AddUpdate(ActivityLogEntryMark EntryMark, string SessionUserId);
        int Delete(int ActivityLogEntryMarkId, string SessionUserId);
    }
}
