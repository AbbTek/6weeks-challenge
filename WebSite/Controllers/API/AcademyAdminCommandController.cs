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
using WebSite.Controllers.API.BaseControllers;
using WebSite.Controllers.API.Exceptions;

namespace WebSite.Controllers.API
{
    //[Authorize(Roles = "SuperAdmin")]
    public class AcademyAdminCommandController : BaseCommandController
    {
        public void Login([FromBody]MessageLogin login)
        {
            var boxes = MongoRepository.GetCollection<Academy>();
            var filter = Builders<Academy>.Filter;
            var list = new List<FilterDefinition<Academy>>();
            list.Add(filter.Eq("_id", new ObjectId(login.AcademyID)));
            list.Add(filter.Eq("Users.Email", login.Email));
       
            var r = boxes.Find(filter.And(list)).SingleOrDefaultAsync().Result;

            if (r == null)
                throw new APIException(string.Format("The user {0} is not valid for the academy", login.Email));

            var result = SignInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, shouldLockout: false).Result;

            switch (result)
            {
                case SignInStatus.Success:
                    break;
                case SignInStatus.LockedOut:
                    throw new APIException("The user is locked");
                    
                case SignInStatus.RequiresVerification:
                    throw new APIException("The user is require verification");

                case SignInStatus.Failure:
                    throw new APIException("Invalid user or password");

                default:
                    break;
            }


        }

        public class MessageAcademyAdminBase
        {
            public string AcademyID { get; set; }
        }
        public class MessageLogin : MessageAcademyAdminBase
        { 
            public string Email { get; set; }
            public string Password { get; set; }
            public bool RememberMe { get; set; }
        }
    }
}
