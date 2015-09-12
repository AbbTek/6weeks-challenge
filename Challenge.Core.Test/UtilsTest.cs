using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text.RegularExpressions;
using Challenge.Core.Utils;

namespace Challenge.Core.Test
{
    [TestClass]
    public class UtilsTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),"Proving invalid names")]
        public void TestNormalizeFile()
        {
            var file1 = FileNameNormalizer.TimestampNormalizer("Carlos file as sdf sdfs saee foto 19.jpG");
            var file2 = FileNameNormalizer.TimestampNormalizer("Ca·#@¬∞¢#@asfs.1.jpG");
            var file3 = FileNameNormalizer.TimestampNormalizer("      .jpG");
        }
    }
}
