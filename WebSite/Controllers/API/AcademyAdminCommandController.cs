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
using Challenge.Core.Domain;
using System.Collections;
using System;

namespace WebSite.Controllers.API
{
    //[Authorize(Roles = "SuperAdmin")]
    public class AcademyAdminCommandController : ApiController
    {
        public void Login([FromBody]Dictionary<string, object>login)
        {

        }   
    }
}
