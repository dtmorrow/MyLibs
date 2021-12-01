using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLibs.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibs.IO.Tests
{
    [TestClass]
    public class FileUtilitiesTests
    {
        [TestMethod]
        public void SanitizeFileNameTest()
        {
            const string badName = @"\/:*?""<>|";

            var sanitizedTrimmed = FileUtilities.SanitizeFileName("   good-file.txt    ");
            var sanitizedTrailing = FileUtilities.SanitizeFileName("good-file.txt...");
            var sanitizedNoCustom = FileUtilities.SanitizeFileName(badName);
            var sanitizedMyDefault = FileUtilities.SanitizeFileNameCustom(badName);

            var custom = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("\\", "a"),
                new KeyValuePair<string, string>("/", "b"),
                new KeyValuePair<string, string>(":", "c"),
                new KeyValuePair<string, string>("*", "d"),
                new KeyValuePair<string, string>("?", "e"),
                new KeyValuePair<string, string>("\"", "f"),
                new KeyValuePair<string, string>("<", "g"),
                new KeyValuePair<string, string>(">", "h"),
                new KeyValuePair<string, string>("|", "i"),
            };
            var sanitizedCustom = FileUtilities.SanitizeFileName(badName, custom);

            Assert.AreEqual("good-file.txt", sanitizedTrimmed);
            Assert.AreEqual("good-file.txt", sanitizedTrailing);
            Assert.AreEqual("_________", sanitizedNoCustom);
            Assert.AreEqual("-- - _.'___", sanitizedMyDefault);
            Assert.AreEqual("abcdefghi", sanitizedCustom);
        }
    }
}