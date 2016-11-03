using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ammo.Portal.Controllers
{
    public class BaseController : Controller
    {
        public Guid UserId
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    return Guid.Parse(User.Identity.GetUserId());
                }

                return Guid.Empty;
            }
        }
    }
}