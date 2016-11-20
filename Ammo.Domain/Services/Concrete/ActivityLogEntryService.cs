using Ammo.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ammo.Domain.Entities;
using Ammo.Domain.Repositories.Abstract;

namespace Ammo.Domain.Services.Concrete
{
    public class ActivityLogEntryService : BaseService, IActivityLogEntryService
    {
        private IActivityLogEntryRepository _entryRepository;

        public ActivityLogEntryService(IActivityLogEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }

        public int AddUpdate(ActivityLogEntry Entry, string SessionUserId)
        {
            return _entryRepository.AddUpdate(Entry, SessionUserId);
        }

        public int Delete(int ActivityLogEntryId, string SessionUserId)
        {
            return _entryRepository.Delete(ActivityLogEntryId, SessionUserId);
        }

        public IEnumerable<ActivityLogEntry> Get(int? ActivityLogEntryId, int? ActivityLogId, int? ActivityId)
        {
            return _entryRepository.Get(ActivityLogEntryId, ActivityLogId, ActivityId);
        }
    }
}
