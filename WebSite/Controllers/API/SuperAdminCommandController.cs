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

        public void CreateOrUpdateBox([FromBody]Box box)
        {
            var boxes = MongoRepository.GetCollection<Box>();
            if (string.IsNullOrWhiteSpace(box._id))
            {
                boxes.InsertOneAsync(box).Wait();
            }
            else
            {
                boxes.ReplaceOneAsync<Box>(z => z._id == box._id, box).Wait();
            }
        }

        public void DeleteBox(string id)
        {
            var boxes = MongoRepository.GetCollection<Box>();
            var filter = Builders<Box>.Filter.Eq("_id", new ObjectId(id));
            boxes.DeleteOneAsync(filter).Wait();
        }
    }
}
