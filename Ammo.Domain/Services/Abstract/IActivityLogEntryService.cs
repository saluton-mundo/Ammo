using Ammo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ammo.Domain.Services.Abstract
{
    public interface IActivityLogEntryService
    {
        IEnumerable<ActivityLogEntry> Get(int? ActivityLogEntryId, int? ActivityLogId, int? ActivityId);
        int AddUpdate(ActivityLogEntry Entry, string SessionUserId);
        int Delete(int ActivityLogEntryId, string SessionUserId);
    }
}
