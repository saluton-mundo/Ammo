using Ammo.Domain.Entities;
using Ammo.Domain.Services.Abstract;
using Ammo.Portal.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ammo.Portal.Controllers
{
    [Authorize]
    [RoutePrefix("Journal")]
    [Route("{action=index}")]
    public class JournalController : BaseController
    {
        private IJournalService _journalService;
        private IJournalIndexService _indexService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="journalService"></param>
        public JournalController(IJournalIndexService indexService,
                                 IJournalService journalService)
        {
            _indexService = indexService;
            _journalService = journalService;
        }

        /// <summary>
        /// Index action
        /// </summary>
        /// <param name="JournalId"></param>
        [Route("{JournalId}")]
        public ActionResult Index(int JournalId)
        {
            JournalIndex model = _indexService.Get(JournalId, User.Identity.GetUserId());

            return View("Index", model);
        }

        /// <summary>
        /// Specific Page of Journal
        /// </summary>
        /// <param name="PageNo"></param>
        /// <returns></returns>
        public ActionResult Page(int JournalId, int PageNo)
        {
            JournalPage page = new JournalPage();

            return View("Page", page);
        }

        /// <summary>
        /// Create a new Journal
        /// </summary>
        [Route]
        public ActionResult Create()
        {
            Journal model = new Journal()
            {
                OwnerId = User.Identity.GetUserId(),
                LastEdited = DateTime.Now,
                PageCount = 0,
                Title = "New Ammo Journal"
            };
            
            if(Request.IsAjaxRequest())
            {
                return PartialView("_Create", model);
            }

            return View("Create", model);
        } 

        [Route("Delete/{JournalId}")]
        public ActionResult Delete(int JournalId)
        {
            JournalDeleteViewModel model = new JournalDeleteViewModel()
            {
                JournalId = JournalId
            };

            return PartialView("_Delete", model);
        }

        [Route]
        public ActionResult Archive(JournalDeleteViewModel request)
        {
            if(request.JournalId == 0)
            {
                return View("_Delete", request);
            }

            _journalService.Delete(request.JournalId, User.Identity.GetUserId());

            return RedirectToAction("Index", "Journals", new { UserName = User.Identity.GetUserId() });
        }

        [Route("Edit/{JournalId}")]
        public ActionResult Edit(int JournalId)
        {
            Journal journal = _journalService.Get(JournalId, User.Identity.GetUserId()).FirstOrDefault();

            if(journal == null || JournalId == 0)
            {
                return View();
            }

            return View("Edit", journal);
        }

        /// <summary>
        /// Saves the journal
        /// </summary>
        [HttpPost]
        [Route]
        public ActionResult Save(Journal journal)
        {
            if(ModelState.IsValid)
            {
                int journalId = _journalService.AddUpdate(journal, User.Identity.GetUserId());

                return RedirectToAction("Index", "Journals");
            }

            return Json(new
            {
                result = "Error"
            });
        }
    }
}