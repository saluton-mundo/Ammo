using Ammo.Domain.Entities;
using Ammo.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ammo.Portal.Controllers
{
    public class JournalBulletCollectionController : BaseController
    {
        IJournalBulletCollectionService _journalBulletCollectionService;

        public JournalBulletCollectionController(IJournalBulletCollectionService journalBulletCollectionService)
        {
            _journalBulletCollectionService = journalBulletCollectionService;
        }

        [Authorize]
        [Route("Save")]
        public ActionResult Save(JournalBulletCollection Collection)
        {
            if(ModelState.IsValid)
            {
                //TODO call AddUpdate

                return Json(new
                {
                    result = "Success",
                    notify = false,
                    view = ""
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    result = "Error",
                    notify = true, 
                    view = ""
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}