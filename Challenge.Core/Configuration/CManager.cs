using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Core.Configuration
{
    public static class CManager
    {
        public const string sectionName = "challenge";
        private static ChallengeSettings frameworkSetting = (ChallengeSettings)ConfigurationManager.GetSection(sectionName);

        public static ChallengeSettings Settings
        {
            get { return frameworkSetting; }
        }
    }
}
