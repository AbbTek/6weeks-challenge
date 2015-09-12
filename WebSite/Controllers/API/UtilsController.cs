using System.Collections.Generic;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Web;
using System.Web.Http;
using Challenge.Core.Domain.Security;
using Challenge.Core;
using MongoDB.Driver;
using MongoDB.Bson;
using WebSite.Models;
using System.Net.Http;
using System.Net;
using System.Net.Http.Formatting;
using System.Collections;
using Challenge.Core.Domain;
using System;
using Challenge.Core.Utils;

namespace WebSite.Controllers.API
{
    [Authorize]
    public class UtilsController : ApiController
    {
        public string GetNormalizeFileName(string fileName)
        {
            return FileNameNormalizer.TimestampNormalizer(fileName);
        }
    }
}
