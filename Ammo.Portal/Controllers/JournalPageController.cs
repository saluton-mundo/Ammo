using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ammo.Portal.Controllers
{
    [Authorize]
    [RoutePrefix("Page")]
    public class JournalPageController : BaseController
    {
        // GET: JournalPage
        [Route("Index")]
        public ActionResult Index(int? PageNo = null)
        {
            if(PageNo == null)
            {
                PageNo = 1;
            }

            // Call service and retrieve the desired page

            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            else
            {
                return View();
            }
        }
    }
}