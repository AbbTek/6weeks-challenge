using Challenge.Core.Configuration;
using Challenge.Core.Domain.Security;
using Challenge.Core.Utils;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Core
{
    public static class MongoRepository
    {
        private static IMongoDatabase db;
        static MongoRepository()
        {
            var settings = CManager.Settings;
            var client = new MongoClient(settings.MongoDBConnection);
            db = client.GetDatabase(settings.MongoDBName);
        }
        public static IMongoCollection<BsonDocument>GetCollectionBsonDocument<T>()
        {
            var attribute = GetAttribute<T>();
            if (attribute == null)
                throw new ArgumentException("You must add a CollectionNameAttribute to your class");

            return db.GetCollection<BsonDocument>(attribute.Name);
        }

        public static IMongoCollection<T> GetCollection<T>()
        {
            var attribute = GetAttribute<T>();
            if (attribute == null)
                throw new ArgumentException("You must add a CollectionNameAttribute to your class");

            return db.GetCollection<T>(attribute.Name);
        }

        private static CollectionNameAttribute GetAttribute<T>()
        {
            return (CollectionNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(CollectionNameAttribute));
        }
    }
}
