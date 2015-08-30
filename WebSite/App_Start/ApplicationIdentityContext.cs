using AspNet.Identity.MongoDB;
using Challenge.Core.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSite.Models;

namespace WebSite.App_Start
{
    public class ApplicationIdentityContext : IDisposable
    {
        public static ApplicationIdentityContext Create()
        {
            // todo add settings where appropriate to switch server & database in your own application
            var config = CManager.Settings;
            var client = new MongoClient(config.MongoDBConnection);
            var database = client.GetDatabase(config.MongoDBName);
            var users = database.GetCollection<ApplicationUser>("users");
            var roles = database.GetCollection<IdentityRole>("roles");
            return new ApplicationIdentityContext(users, roles);
        }

        private ApplicationIdentityContext(IMongoCollection<ApplicationUser> users, IMongoCollection<IdentityRole> roles)
        {
            Users = users;
            Roles = roles;
        }

        public IMongoCollection<IdentityRole> Roles { get; set; }

        public IMongoCollection<ApplicationUser> Users { get; set; }

        public Task<List<IdentityRole>> AllRolesAsync()
        {
            return Roles.Find(r => true).ToListAsync();
        }

        public void Dispose()
        {
        }
    }
}