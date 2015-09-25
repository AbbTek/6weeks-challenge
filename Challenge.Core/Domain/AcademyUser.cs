using Challenge.Core.Domain.Security;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Challenge.Core.Domain
{
    public class AcademyUser
    {
        public string Email { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        [BsonRepresentation(BsonType.String)]
        public Role Role { get; set; }
    }
}
