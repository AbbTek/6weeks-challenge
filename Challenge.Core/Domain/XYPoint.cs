using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Core.Domain
{
    public class XYPoint
    {
        [JsonProperty("type")]
        [BsonElement("type")]
        public string Type
        {
            get
            {
                return "Point";
            }
        }

        [JsonProperty("coordinates")]
        [BsonElement("coordinates")]
        public double[] Coordinates
        {
            get;
            set;
        }

        public XYPoint(double x, double y)
        {
            Coordinates = new[] { x, y };
        }

        [BsonIgnore]
        [JsonIgnore]
        public double X { get { return Coordinates[0]; } }

        [BsonIgnore]
        [JsonIgnore]
        public double Y { get { return Coordinates[1]; } }
    }
}
