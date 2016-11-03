using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ammo.Portal.Controllers
{
    [Authorize]
    public class JournalTagController : BaseController
    {
        // GET: JournalTag
        public ActionResult Index()
        {
            return View();
        }
    }
}