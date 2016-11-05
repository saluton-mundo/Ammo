using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ammo.Portal.Controllers
{
    // TODO Make the paths config!
    public class ImageController : BaseController
    {
        [HttpGet]
        public ActionResult Get(string fileName)
        {
            var dir = Server.MapPath("/Media");
            var path = Path.Combine(dir, fileName + ".jpg");
            return base.File(path, "image/jpeg");
        }

        [HttpGet]
        public FileResult DefaultCoverGet()
        {
            string path = Server.MapPath("/Assets/images/backgrounds/blank-cover.png");

            return new FileStreamResult(new FileStream(path, FileMode.Open), "image/png");
        }

        [HttpGet]
        [Authorize]
        public FileResult JournalCoverGet(string ImageName = null)
        {
            if(string.IsNullOrEmpty(ImageName))
            {
                return DefaultCoverGet();
            }
            else
            {
                string path = Server.MapPath(string.Concat("/Media/ImageUploads/JournalCovers/", ImageName));

                return new FileStreamResult(new FileStream(path, FileMode.Open), "image/png");
            }        
        }

        [HttpGet]
        [Authorize]
        public FileResult BulletGet(string BulletUri = null)
        {
            if(string.IsNullOrEmpty(BulletUri))
            {
                return new FileStreamResult(new FileStream("/Assets/icons/bullet.png", FileMode.Open), "image/[ng");
            }
            else
            {
                string path = Server.MapPath(string.Concat("/Assets/bullets/", BulletUri));

                return new FileStreamResult(new FileStream(path, FileMode.Open), "image/png");
            }
        }
    }
}