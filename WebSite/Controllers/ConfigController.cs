using Challenge.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebSite.Models.Config;

namespace WebSite.Controllers
{
    public class ConfigController : Controller
    {
        // GET: Config
        public ActionResult Core()
        {
            var settings = CManager.Settings;
            var values = new 
            {
                AWSAccessKeyId = settings.AWSAccessKeyID,
                UrlS3 = settings.AWSS3Upload.URL,
                UploadPolicy = settings.AWSS3Upload.GetPolicyInBase64(),
                UploadSignature = settings.AWSS3Upload.GetSignature()
            };

            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(values);

            var config = new GenericConfig()
            {
                Module = "6weekschallenge.core",
                Settings = json
            };

            Response.ContentType = "text/javascript";
            return View(config);
        }

        public ActionResult AcademyAdmin(string id)
        {
            var values = new
            {
                AcademyID = id
       
            };

            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(values);

            var config = new GenericConfig()
            {
                Module = "6weekschallenge.academyAdmin",
                Settings = json
            };

            Response.ContentType = "text/javascript";
            return View(config);
        }
    }

}