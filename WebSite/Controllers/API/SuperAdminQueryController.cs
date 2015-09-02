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

namespace WebSite.Controllers.API
{
    //[Authorize(Roles = "SuperAdmin")]
    public class SuperAdminQueryController : ApiController
    {
        public IEnumerable<User> GetAllUsers()
        {
            var users = MongoRepository.GetCollection<User>();
            return users.Find(new BsonDocument()).ToListAsync().Result;
        }

        public IEnumerable<IDictionary> GetAllUsers(string projections)
        {
            var users = MongoRepository.GetCollectionBsonDocument<User>();
            var r = users.Find(new BsonDocument()).Project<BsonDocument>(projections).ToListAsync().Result;
            foreach (var item in r)
            {
                yield return item.ToDictionary();
            }
        }

        public IEnumerable<IDictionary> GetAllBoxes(string projections)
        {
            var users = MongoRepository.GetCollectionBsonDocument<Box>();
            var r = users.Find(new BsonDocument()).Project<BsonDocument>(projections).ToListAsync().Result;
            foreach (var item in r)
            {
                yield return item.ToDictionary();
            }
        }

    }
}
