using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Challenge.Core.Configuration;
using Challenge.Core.Domain;
using MongoDB.Bson;

namespace Challenge.Core.Test.Domain
{
    [TestClass]
    public class SetupTest
    {
        [TestMethod]
        public void Start()
        {
            var settings = CManager.Settings;
            var client = new MongoClient(settings.MongoDBConnection);
            var db = client.GetDatabase(settings.MongoDBName);
            var boxes = db.GetCollection<Box>("boxes");
            //boxes.InsertOneAsync(new Box() { Name = "RMA2", Address = "Test Address" }).Wait();

            var list = boxes.Find(new BsonDocument()).ToListAsync().Result;
         }
    }
}
