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
using WebSite.Controllers.API.Messages;
using WebSite.Controllers.API.Exceptions;
using Challenge.Core.Utils;
using WebSite.Controllers.API.BaseControllers;

namespace WebSite.Controllers.API
{
    [Authorize(Roles = "SuperAdmin")]
    public class SuperAdminCommandController : BaseCommandController
    {
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
            academy.State = AcademyState.Draft;
            academy.Users = new AcademyUser[] { };
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

        public void ActivateAcademy([FromBody]BaseID message)
        {
            var boxes = MongoRepository.GetCollection<Academy>();
            var filter = Builders<Academy>.Filter.Eq("_id", new ObjectId(message.id));
            var academy = boxes.Find<Academy>(filter).Project<Academy>("{ State : 1,  EmailManager: 1}").SingleOrDefaultAsync().Result;

            if (academy.State == AcademyState.Draft)
            {
                var builder = Builders<Academy>.Update;
                var update = new List<UpdateDefinition<Academy>>();

                update.Add(builder.Set("State", AcademyState.Active));
                update.Add(builder.Push("Users", new AcademyUser() { Email = academy.EmailManager, Role = Role.BoxAdmin }));

                var r = boxes.UpdateOneAsync(filter, builder.Combine(update)).Result;

                var user = UserManager.FindByEmailAsync(academy.EmailManager).Result;

                if (user == null)
                {
                    var appUser = new ApplicationUser { UserName = academy.EmailManager, Email = academy.EmailManager };
                    appUser.AddRole(EnumsUtils.GetName(Role.BoxAdmin));
                    var result = UserManager.CreateAsync(appUser, "Revolute2015!").Result;
                }
            }
            else
            {
                throw new APIException("You can only activate a Draft academy");
            }

        }
    }
}
