using Ammo.Domain.Repositories.Abstract;
using Ammo.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ammo.Domain.Entities;

namespace Ammo.Domain.Services.Concrete
{
    public class ActivityLogActivityService : BaseService, IActivityLogActivityService
    {
        private IActivityLogActivityRepository _activityRepository;

        public ActivityLogActivityService(IActivityLogActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public int AddUpdate(ActivityLogActivity Activity, string SessionUserId)
        {
            if(Activity == null || string.IsNullOrEmpty(SessionUserId))
            {
                return 0;
            }

            return _activityRepository.AddUpdate(Activity, SessionUserId);
        }

        public int Delete(int ActivityLogActivityId, string SessionUserId)
        {
            if(ActivityLogActivityId == 0 || string.IsNullOrEmpty(SessionUserId))
            {
                return 0;
            }

            return _activityRepository.Delete(ActivityLogActivityId, SessionUserId);
        }

        public IEnumerable<ActivityLogActivity> Get(int ActivityLogId, int? ActivityLogActivityId)
        {
            return _activityRepository.Get(ActivityLogId, ActivityLogActivityId);
        }
    }
}
