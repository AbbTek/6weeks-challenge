using Challenge.Core.Configuration;
using Challenge.Core.Utils;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

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

        public static UpdateDefinitionResult GetUpdateDefinitions(IDictionary<string,object> fields)
        {
            return GetUpdateDefinitions(null, fields);
        }

        private static UpdateDefinitionResult GetUpdateDefinitions(string rootName, IDictionary<string, object> fields)
        {
            var builder = Builders<BsonDocument>.Update;
            var update = new List<UpdateDefinition<BsonDocument>>();
            var _id = ObjectId.Empty;

            foreach (var item in fields)
            {
                var complexObject = item.Value as JObject;
                if (complexObject != null)
                {
                    var newDictionary = new Dictionary<string, object>();
                    foreach (var itemObject in complexObject)
                    {
                        newDictionary[itemObject.Key] = itemObject.Value.ToObject<object>();
                    }
                    update.AddRange(GetUpdateDefinitions(item.Key, newDictionary).UpdateDefinitionList);
                } else if (item.Key == "_id")
                {
                    _id = new ObjectId((string)item.Value);
                }
                else
                {
                    var key = string.IsNullOrWhiteSpace(rootName) ? item.Key : rootName + "." + item.Key;

                    var array = item.Value as JArray;
                    if (array != null)
                    {
                        for (int i = 0; i < array.Count; i++)
                        {
                            update.Add(builder.Set(key + "." + i, array[i].ToObject<object>()));
                        }
                    }
                    else
                    {
                        update.Add(builder.Set(key, item.Value));
                    }
                }
            }
            return new UpdateDefinitionResult() {
                _id = _id,
                UpdateDefinitionList = update
            };
        }

    }

    public class UpdateDefinitionResult
    {
        public ObjectId _id { get; set; }
        public List<UpdateDefinition<BsonDocument>> UpdateDefinitionList { get; set; }
    }
}
