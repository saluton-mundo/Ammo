using Ammo.Domain.Entities;
using Ammo.Domain.Services.Abstract;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ammo.Portal.Controllers
{
    [Authorize]
    public class BulletCollectionController : BaseController
    {
        private IBulletCollectionService _collectionService;

        public BulletCollectionController(IBulletCollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [Route("List")]
        public ActionResult List()
        {
            IEnumerable<BulletCollection> model = _collectionService.GetByUser(User.Identity.GetUserId());

            return PartialView("_List", model);
        }
    }
}