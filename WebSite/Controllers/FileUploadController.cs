using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        [HttpPost]
        public ContentResult Index(HttpPostedFileBase file)
        {
            var originalFilename = Path.GetFileName(file.FileName);
            var extension = Path.GetExtension(originalFilename);
            var rand = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
            var filename = DateTime.Now.ToString("yyyyMMddHHmm") + "-" + rand + extension;

            var path = Path.Combine(Server.MapPath("~/Uploads"), filename);
            file.SaveAs(path);
            return new ContentResult
            {
                ContentType = "text/plain",
                Content = "/Uploads/" + filename,
                ContentEncoding = Encoding.UTF8
            };
        }
    }
}