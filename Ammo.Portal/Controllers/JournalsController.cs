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
    public class JournalsController : BaseController
    {
        private IJournalService _journalsService;

        /// <summary>
        /// Constructor with Service injected
        /// </summary>
        /// <param name="journalsService"></param>
        public JournalsController(IJournalService journalsService)
        {
            _journalsService = journalsService;
        }

        // GET: Journals
        [Authorize]
        public ActionResult Index(string UserName = null)
        {
            if(string.IsNullOrEmpty(UserName))
            {
                UserName = User.Identity.GetUserId();
            }

            IEnumerable<Journal> model = _journalsService.Get(null, UserName);

            return View("Index", model);
        }
    }
}