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

namespace WebSite.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminServicesController : ApiController
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

        public IEnumerable<User> GetAllUsers()
        {
            var p = new Persistence();
            var users = p.GetCollection<User>("users");
            return users.Find(new BsonDocument()).ToListAsync().Result;
        }

        public void CreateUser([FromBody]User user)
        {
            var appUser = new ApplicationUser { UserName = user.Email, Email = user.Email };
            var result = UserManager.CreateAsync(appUser, "Abbatekkkk12@").Result;
        }

        public void DeleteUser(string id)
        {
            var p = new Persistence();
            var users = p.GetCollection<User>("users");
            var filter = Builders<User>.Filter.Eq("Id", new ObjectId(id));
            var projection = Builders<User>.Projection.Include("Email");
            var userEmail = users.Find(filter).Project(projection).FirstOrDefaultAsync().Result;
            var user = UserManager.FindByEmailAsync(userEmail["Email"].AsString).Result;
            var result = UserManager.DeleteAsync(user).Result;
        }
    }
}
