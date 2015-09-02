using Challenge.Core.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Core.Domain.Security
{
    [CollectionName("users")]
    [BsonIgnoreExtraElements]
    public class User
    {
        public ObjectId Id { get; set; }
        public string Email { get; set; }
        [BsonIgnoreIfNull]
        public string UserName { get; set; }
        public bool EmailConfirmed { get; set; }
        public IList<String> Roles { get; set; }
    }
}
