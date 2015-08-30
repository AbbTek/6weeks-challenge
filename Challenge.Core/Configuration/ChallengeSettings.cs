using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Core.Configuration
{
    public class ChallengeSettings : ConfigurationSection
    {
        private const string mongoDBConnection = "dbConnection";
        private const string mongoDBName = "dbName";

        [ConfigurationProperty(mongoDBConnection)]
        public string MongoDBConnection
        {
            get { return (string)this[mongoDBConnection]; }
        }

        [ConfigurationProperty(mongoDBName)]
        public string MongoDBName
        {
            get { return (string)this[mongoDBName]; }
        }
    }
}
