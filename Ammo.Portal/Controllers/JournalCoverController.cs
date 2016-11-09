using Ammo.Domain.Entities;
using Ammo.Domain.Services.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Ammo.Portal.Controllers
{
    [Authorize]
    [RoutePrefix("JournalCover")]
    public class JournalCoverController : BaseController
    {
        private IJournalCoverService _coverService;

        public JournalCoverController(IJournalCoverService coverService)
        {
            _coverService = coverService;
        }

        // GET: JournalCover
        [Route("Upload")]
        public HttpResponseMessage Upload(HttpRequestMessage request, int coverid)
        {
            HttpResponseMessage response = null;

            // Get Journal 

            // If null return not found 

            // else upload and return 
            
            return response;

        }
    }
}