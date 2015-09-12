using Challenge.Core.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Core.Domain
{
    [CollectionName("boxes")]
    [BsonIgnoreExtraElements]
    public class Box
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
        public string EmailManager { get; set; }
        public string ShortName { get; set; }
        public string UrlLogo { get; set; }
        public XYPoint Location { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
