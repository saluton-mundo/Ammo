using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ammo.Portal.Controllers
{
    [Authorize]
    public class JournalPageTemplateController : BaseController
    {
        // GET: JournalPageTemplate
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }
    }
}