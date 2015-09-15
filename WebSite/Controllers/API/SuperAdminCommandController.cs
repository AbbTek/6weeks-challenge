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
    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminCommandController : ApiController
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public void CreateUser([FromBody]User user)
        {
            var appUser = new ApplicationUser { UserName = user.Email, Email = user.Email };
            var result = UserManager.CreateAsync(appUser, "Abbatekkkk12@").Result;
        }

        public void DeleteUser(string id)
        {
            var users = MongoRepository.GetCollection<User>();
            var filter = Builders<User>.Filter.Eq("Id", new ObjectId(id));
            var projection = Builders<User>.Projection.Include("Email");
            var userEmail = users.Find(filter).Project(projection).FirstOrDefaultAsync().Result;
            var user = UserManager.FindByEmailAsync(userEmail["Email"].AsString).Result;
            var result = UserManager.DeleteAsync(user).Result;
        }

        public void CreateAcademy([FromBody]Academy academy)
        {
            var boxes = MongoRepository.GetCollection<Academy>();
            academy.CreationDate = DateTime.Now;
            boxes.InsertOneAsync(academy).Wait();
        }

        public void UpdateAcademy([FromBody]Dictionary<string, object> academy)
        {
            var updateDefinition = MongoRepository.GetUpdateDefinitions(academy);
            var boxes = MongoRepository.GetCollectionBsonDocument<Academy>();
            var builder = Builders<BsonDocument>.Update;

            updateDefinition.UpdateDefinitionList.Add(builder.CurrentDate("LastUpdate"));
            
            var filter = Builders<BsonDocument>.Filter.Eq("_id", updateDefinition._id);
            var r = boxes.UpdateOneAsync(filter, builder.Combine(updateDefinition.UpdateDefinitionList)).Result;
        }

        public void DeleteAcademy(string id)
        {
            var boxes = MongoRepository.GetCollection<Academy>();
            var filter = Builders<Academy>.Filter.Eq("_id", new ObjectId(id));
            boxes.DeleteOneAsync(filter).Wait();
        }
    }
}
