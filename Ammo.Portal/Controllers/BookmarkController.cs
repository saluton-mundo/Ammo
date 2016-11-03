using Ammo.Domain.Entities;
using Ammo.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ammo.Portal.Controllers
{
    [RoutePrefix("Bookmark")]
    public class BookmarkController : BaseController
    {
        private IBookmarkService _bookmarkService;

        public BookmarkController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        [Authorize]
        [Route]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [Route("List/{JournalId}")]
        public ActionResult List(int JournalId)
        {
            // Here we will get all bookmarks relating to this journal
            IEnumerable<Bookmark> model = new List<Bookmark>();
            return View("List", model);
        }
    }
}