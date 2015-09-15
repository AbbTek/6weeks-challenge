using Challenge.Core;
using Challenge.Core.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models.AcademyAdmin;

namespace WebSite.Controllers
{
    public class AcademyAdminController : Controller
    {
        // GET: AcademyAdmin
        public ActionResult Index(string id)
        {
            var academies = MongoRepository.GetCollection<Academy>();
            var builder = Builders<Academy>.Filter;
            var filter = builder.Eq(a => a.ShortName, id);
            var academy = academies.Find<Academy>(filter).SingleOrDefaultAsync().Result;

            return View(new AcademyModel() {
                ID = academy._id,
                UrlLogo = academy.UrlLogo,
                Name = academy.Name
            });
        }

        public ActionResult Dashboard(string id)
        {
            return View();
        }
    }
}