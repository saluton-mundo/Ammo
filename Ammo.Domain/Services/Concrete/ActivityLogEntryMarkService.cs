using Ammo.Domain.Services.Abstract;
using Ammo.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ammo.Domain.Entities;

namespace Ammo.Domain.Services.Concrete
{
    public class ActivityLogEntryMarkService : BaseService, IActivityLogEntryMarkService
    {
        private IActivityLogEntryMarkRepository _markRepository;

        public ActivityLogEntryMarkService(IActivityLogEntryMarkRepository markRepository)
        {
            _markRepository = markRepository;
        }

        public int AddUpdate(ActivityLogEntryMark EntryMark, string SessionUserId)
        {
            if(IsValid(EntryMark) && string.IsNullOrEmpty(SessionUserId) == false)
            {
                return _markRepository.AddUpdate(EntryMark, SessionUserId);
            }

            return 0;
        }

        public int Delete(int ActivityLogEntryMarkId, string SessionUserId)
        {
            if(ActivityLogEntryMarkId == 0 || string.IsNullOrEmpty(SessionUserId))
            {
                return 0;
            }

            return _markRepository.Delete(ActivityLogEntryMarkId, SessionUserId);
        }

        public IEnumerable<ActivityLogEntryMark> Get(int? ActivityLogId, int? ActivityLogEntryMarkId)
        {
            return _markRepository.Get(ActivityLogId, ActivityLogEntryMarkId);
        }

        private bool IsValid(ActivityLogEntryMark EntryMark)
        {
            bool isValid = true;

            // Not associated with a Log!
            if(EntryMark.ActivityLogId == 0)
            {
                return false;
            }
            // Not a valid Hex color 
            if(EntryMark.Color.Length < 7)
            {
                return false;
            }
            // No meaning given to mark!
            if(string.IsNullOrEmpty(EntryMark.Description))
            {
                return false;
            }

            return isValid;
        }
    }
}
