using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Challenge.Core.Configuration
{
    public class AWSS3upload : ConfigurationElement 
    {
        private const string url = "url";
        private const string keyFormat = "keyFormat";
        private const string policy = "policy";

        [ConfigurationProperty(url)]
        public string URL
        {
            get { return (string)this[url]; }
        }

        [ConfigurationProperty(keyFormat)]
        public string KeyFormat
        {
            get { return (string)this[keyFormat]; }
        }

        [ConfigurationProperty(policy)]
        public string Policy
        {
            get { return (string)this[policy]; }
        }
    }
}
