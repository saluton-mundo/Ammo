using Ammo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ammo.Portal.Controllers
{
    [Authorize]
    [RoutePrefix("ActivityLogActivity")]
    public class ActivityLogActivityController : BaseController
    {
        [Route("Save")]
        public ActionResult Save()
        {

            return View();
        }

        [Route("Create")]
        public ActionResult Create(int ActivityLogId)
        {
            ActivityLogActivity activity = new ActivityLogActivity()
            {
                ActivityLogId = ActivityLogId,
                Description = ""
            };

            return PartialView("_Create", activity);
        }

        [Route("Edit")]
        public ActionResult Edit(int ActivityLogId, int ActivityId)
        {
            return PartialView("_Edit");
        }
    }
}