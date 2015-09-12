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
        private const string awsAccessKeyID = "awsAccessKeyID";
        private const string awsSecretAccessKey = "awsSecretAccessKey";
        private const string awsS3Upload = "awsS3Upload";

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

        [ConfigurationProperty(awsAccessKeyID)]
        public string AWSAccessKeyID
        {
            get { return (string)this[awsAccessKeyID]; }
        }

        [ConfigurationProperty(awsSecretAccessKey)]
        public string AWSSecretAccessKey
        {
            get { return (string)this[awsSecretAccessKey]; }
        }

        [ConfigurationProperty(awsS3Upload)]
        public AWSS3upload AWSS3Upload
        {
            get { return (AWSS3upload)this[awsS3Upload]; }
        }
    }
}
