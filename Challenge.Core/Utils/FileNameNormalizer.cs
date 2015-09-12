using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Challenge.Core.Utils
{
    public static class FileNameNormalizer
    {
        private static Regex regex;

        static FileNameNormalizer()
        {
            //string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            regex = new Regex(@"[^a-z\d]");
        }

        public static string TimestampNormalizer(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("FileName cannot be null");

            var onlyName = Path.GetFileNameWithoutExtension(fileName).ToLowerInvariant();
            var extension = Path.GetExtension(fileName).ToLowerInvariant();

            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            onlyName = regex.Replace(onlyName, "");

            if (onlyName.Length > 10)
                onlyName = onlyName.Substring(0, 10);

            if (string.IsNullOrWhiteSpace(onlyName))
                throw new ArgumentException(string.Format("Filename invalid '{0}'", fileName));

            var timestamp = DateTime.Now.ToString("fff-ss-mm-HH-dd-MM-yyyy");
            return timestamp + "-" + onlyName + extension;
        }
    }
}
