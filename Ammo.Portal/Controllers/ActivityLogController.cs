using Ammo.Domain.Entities;
using Ammo.Domain.Services.Abstract;
using Ammo.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ammo.Portal.Controllers
{
    [Authorize]
    [RoutePrefix("ActivityLog")]
    public class ActivityLogController : BaseController
    {
        IActivityLogService _logService;

        public ActivityLogController(IActivityLogService logService)
        {
            _logService = logService;
        }

        [Route("Index")]
        public ActionResult Index()
        {
            return View("Index", CreateDebugViewModel());
        }

        #region FOR DEBUG PURPOSES
        private IEnumerable<ActivityLogActivity> CreateDebugActivities()
        {
            List<ActivityLogActivity> activities = new List<ActivityLogActivity>();
            ActivityLogActivity running = new ActivityLogActivity() { ActivityId = 1, ActivityLogId = 1, Description = "Running" };
            ActivityLogActivity smoking = new ActivityLogActivity() { ActivityId = 2, ActivityLogId = 1, Description = "Not Smoking" };
            ActivityLogActivity meditation = new ActivityLogActivity() { ActivityId = 3, ActivityLogId = 1, Description = "Meditation" };
            ActivityLogActivity gym = new ActivityLogActivity() { ActivityId = 1, ActivityLogId = 1, Description = "Gym" };
            ActivityLogActivity coding = new ActivityLogActivity() { ActivityId = 1, ActivityLogId = 1, Description = "Coding Ammo" };

            activities.Add(running);
            activities.Add(smoking);
            activities.Add(meditation);
            activities.Add(gym);
            activities.Add(coding);

            return activities;
        }

        private IEnumerable<ActivityLogEntryMark> CreateDebugEntryMarks()
        {
            List<ActivityLogEntryMark> marks = new List<ActivityLogEntryMark>();

            ActivityLogEntryMark good = new ActivityLogEntryMark() { ActivityEntryLogMarkId = 1, ActivityLogId = 1, Color = "#38c695", Description = "Did It!" };
            ActivityLogEntryMark notApplicable = new ActivityLogEntryMark() { ActivityEntryLogMarkId = 2, ActivityLogId = 1, Color = "#b27cf5", Description = "Not Scheduled" };
            ActivityLogEntryMark partial = new ActivityLogEntryMark() { ActivityEntryLogMarkId = 3, ActivityLogId = 1, Color = "#feb960", Description = "Partially" };
            ActivityLogEntryMark bad = new ActivityLogEntryMark() { ActivityEntryLogMarkId = 4, ActivityLogId = 1, Color = "#fc5f45", Description = "Forgot" };

            marks.Add(good);
            marks.Add(notApplicable);
            marks.Add(partial);
            marks.Add(bad);
            
            return marks;
        }

        private ActivityLogViewModel CreateDebugViewModel()
        {
            return new ActivityLogViewModel()
            {
                JournalId = 4,
                ActivityLog = new ActivityLog()
                {
                    Activities = CreateDebugActivities(),
                    JournalId = 4,
                    JournalPageNo = 1,
                    JournalPageSortOrder = 1,
                    LogMonth = DateTime.Now,
                    OwnerId = Startup.Admin
                },
                Entries = new List<ActivityLogEntry>(),
                Marks = CreateDebugEntryMarks()
            };
        }
        #endregion
    } 
}