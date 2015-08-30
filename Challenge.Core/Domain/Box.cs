using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Core.Domain
{
    public class Box
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        [BsonIgnoreIfDefault]
        public string Address { get; set; }
    }
}
