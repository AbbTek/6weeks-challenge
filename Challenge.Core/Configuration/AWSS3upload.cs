using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Security.Cryptography;

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

        public string GetPolicyInBase64()
        {
            var policy = Policy.Replace("[[DATE]]", DateTime.Now.AddYears(1).ToString("yyyy-MM-ddT00:00:00Z"));
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(policy));
        }

        public string GetSignature()
        {
            var policy64 = GetPolicyInBase64();
            byte[] b64Key = Encoding.ASCII.GetBytes(CManager.Settings.AWSSecretAccessKey);
            var hmacSha1 = new HMACSHA1(b64Key);
            return Convert.ToBase64String(hmacSha1.ComputeHash(Encoding.ASCII.GetBytes(policy64)));
        }
    }
}
