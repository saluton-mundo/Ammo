using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ammo.Portal.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if(Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Journals");
            }

            return View();
        }
    }
}