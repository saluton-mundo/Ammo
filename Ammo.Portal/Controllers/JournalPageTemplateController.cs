using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ammo.Portal.Controllers
{
    [Authorize]
    [RoutePrefix("JournalPageTemplate")]
    public class JournalPageTemplateController : BaseController
    {
        // GET: JournalPageTemplate
        [Route("Index")]
        public ActionResult Index()
        {
            return View("Index");
        }

        [Route("List")]
        public ActionResult List()
        {
            return PartialView("_List");
        }
    }
}