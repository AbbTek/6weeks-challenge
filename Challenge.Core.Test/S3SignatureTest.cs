using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Challenge.Core.Configuration;
using System.Text;
using System.Security.Cryptography;

namespace Challenge.Core.Test
{
    [TestClass]
    public class S3SignatureTest
    {
        [TestMethod]
        public void TestMethod()
        {
            var settings = CManager.Settings;
            var policy = settings.AWSS3Upload.Policy.Replace("[[DATE]]", DateTime.Now.AddYears(1).ToString("yyyy-MM-ddTHH:mm:ssZ"));

            string b64Policy = Convert.ToBase64String(Encoding.ASCII.GetBytes(policy));
            byte[] b64Key = Encoding.ASCII.GetBytes(settings.AWSSecretAccessKey);
            var hmacSha1 = new HMACSHA1(b64Key);
            var key = Convert.ToBase64String(hmacSha1.ComputeHash(Encoding.ASCII.GetBytes(b64Policy)));
        }
    }
}
