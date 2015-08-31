using Challenge.Core.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Core
{
    public class Persistence
    {
        private IMongoDatabase db;
        public Persistence()
        {
            var settings = CManager.Settings;
            var client = new MongoClient(settings.MongoDBConnection);
            db = client.GetDatabase(settings.MongoDBName);
        }
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return db.GetCollection<T>(name);
        }
    }
}
