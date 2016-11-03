using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ammo.Portal.Controllers
{
    public class CalendarController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}