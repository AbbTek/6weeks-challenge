using Challenge.Core.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Core.Domain
{
    [CollectionName("academies")]
    [BsonIgnoreExtraElements]
    public class Academy
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [JsonConverter(typeof(StringEnumConverter))] 
        [BsonRepresentation(BsonType.String)]
        public AcademyState State { get; set; }
        public string EmailManager { get; set; }
        public string ShortName { get; set; }
        public string UrlLogo { get; set; }
        public XYPoint Location { get; set; }
        public DateTime? CreationDate { get; set; }
        public AcademyUser[] Users { get; set; }
    }
}
