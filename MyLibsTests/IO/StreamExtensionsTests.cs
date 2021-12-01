using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLibs.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using static MyLibs.Tests.ExceptionUtilities;

namespace MyLibs.IO.Tests
{
    [TestClass]
    public class StreamExtensionsTests
    {
        [TestMethod]
        public void TryReadCharTest()
        {
            using var reader = new StringReader("abc");

            bool result;

            result = reader.TryReadChar(out char c);
            Assert.IsTrue(result);
            Assert.AreEqual('a', c);

            result = reader.TryReadChar(out c);
            Assert.IsTrue(result);
            Assert.AreEqual('b', c);

            result = reader.TryReadChar(out c);
            Assert.IsTrue(result);
            Assert.AreEqual('c', c);

            result = reader.TryReadChar(out c);
            Assert.IsFalse(result);
            Assert.AreEqual('\0', c);

            using StringReader nullReader = null;
            Assert.IsTrue(AreExceptionsEqual(() => nullReader.Read(), () => nullReader.TryReadChar(out c)));
        }

        [TestMethod]
        public void TryPeekCharTest()
        {
            using var reader = new StringReader("abc");

            bool result;

            result = reader.TryPeekChar(out char c);
            Assert.IsTrue(result);
            Assert.AreEqual('a', c);

            reader.Read();

            result = reader.TryPeekChar(out c);
            Assert.IsTrue(result);
            Assert.AreEqual('b', c);

            reader.Read();

            result = reader.TryPeekChar(out c);
            Assert.IsTrue(result);
            Assert.AreEqual('c', c);

            reader.Read();

            result = reader.TryPeekChar(out c);
            Assert.IsFalse(result);
            Assert.AreEqual('\0', c);

            using StringReader nullReader = null;
            Assert.IsTrue(AreExceptionsEqual(() => nullReader.Peek(), () => nullReader.TryPeekChar(out c)));
        }

        [TestMethod]
        public void TryReadCharFullTest()
        {
            var ascii = "a";
            var copyright = "©";
            var unicode = "白上フブキ";
            var emoji = "\U0001F928"; // 🤨
            var mahjong = "\U0001F004"; // 🀄

            var test = $"{ascii}{copyright}{unicode}{emoji}{mahjong}";
            using var reader = new StringReader(test);

            // All of these fit in 1 character
            Assert.IsTrue(reader.TryReadCharFull(out string str) && str == ascii);
            Assert.IsTrue(reader.TryReadCharFull(out str) && str == copyright);

            for (int i = 0; i < unicode.Length; i++)
            {
                Assert.IsTrue(reader.TryReadCharFull(out str) && str == unicode[i].ToString());
            }

            // These should both be 2 characters long
            Assert.IsTrue(reader.TryReadCharFull(out str) && str.Length == 2 && str == emoji);
            Assert.IsTrue(reader.TryReadCharFull(out str) && str.Length == 2 && str == mahjong);

            // Test Exception
            using StringReader nullReader = null;
            Assert.IsTrue(AreExceptionsEqual(() => nullReader.Read(), () => nullReader.TryReadCharFull(out str)));
        }

        [TestMethod]
        public void ReadCStringTest()
        {
            using var reader = new StringReader("abc\0def\0ghi");

            Assert.AreEqual("abc", reader.ReadCString());
            Assert.AreEqual("def", reader.ReadCString());
            Assert.AreEqual("ghi", reader.ReadCString());
            Assert.IsNull(reader.ReadCString());

            using StringReader nullReader = null;
            Assert.IsTrue(AreExceptionsEqual(() => nullReader.ReadLine(), () => nullReader.ReadCString()));
        }

        [TestMethod]
        public void TryReadByteTest()
        {
            var stream = new MemoryStream(new byte[] { 0x01, 0x02, 0x03 });

            bool result;

            result = stream.TryReadByte(out byte b);
            Assert.IsTrue(result);
            Assert.AreEqual(0x01, b);

            result = stream.TryReadByte(out b);
            Assert.IsTrue(result);
            Assert.AreEqual(0x02, b);

            result = stream.TryReadByte(out b);
            Assert.IsTrue(result);
            Assert.AreEqual(0x03, b);

            result = stream.TryReadByte(out b);
            Assert.IsFalse(result);
            Assert.AreEqual(0x00, b);

            MemoryStream streamNull = null;
            Assert.IsTrue(AreExceptionsEqual(() => streamNull.ReadByte(), () => streamNull.TryReadByte(out b)));
        }

        [TestMethod]
        public void CopyToTest()
        {
            var source = new MemoryStream(new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 });
            var destination = new MemoryStream();

            source.CopyTo(destination, 3L);

            destination.Seek(0, SeekOrigin.Begin);

            bool result;

            result = destination.TryReadByte(out byte b);
            Assert.IsTrue(result);
            Assert.AreEqual(0x01, b);

            result = destination.TryReadByte(out b);
            Assert.IsTrue(result);
            Assert.AreEqual(0x02, b);

            result = destination.TryReadByte(out b);
            Assert.IsTrue(result);
            Assert.AreEqual(0x03, b);

            result = destination.TryReadByte(out b);
            Assert.IsFalse(result);
            Assert.AreEqual(0x00, b);

            Assert.IsTrue(AreExceptionsEqual(() => source.CopyTo(destination, -1), () => source.CopyTo(destination, 3L, -1)));

            MemoryStream sourceNull = null;
            MemoryStream destinationNull = null;

            Assert.IsTrue(AreExceptionsEqual(() => sourceNull.CopyTo(destination), () => sourceNull.CopyTo(destination, 3L)));
            Assert.IsTrue(AreExceptionsEqual(() => source.CopyTo(destinationNull), () => source.CopyTo(destinationNull, 3L)));
        }

        [TestMethod]
        public void SkipBytesTest()
        {
            var stream = new MemoryStream(new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 });
            stream.SkipBytes(3);

            bool result;

            result = stream.TryReadByte(out byte b);
            Assert.IsTrue(result);
            Assert.AreEqual(0x04, b);

            result = stream.TryReadByte(out b);
            Assert.IsTrue(result);
            Assert.AreEqual(0x05, b);

            result = stream.TryReadByte(out b);
            Assert.IsFalse(result);
            Assert.AreEqual(0x00, b);

            MemoryStream streamNull = null;
            Assert.IsTrue(AreExceptionsEqual(() => streamNull.Seek(3, SeekOrigin.Current), () => streamNull.SkipBytes(3)));
        }

        [TestMethod]
        public void SkipToEndTest()
        {
            var stream = new MemoryStream(new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 });
            stream.SkipToEnd();

            bool result;

            result = stream.TryReadByte(out byte b);
            Assert.IsFalse(result);
            Assert.AreEqual(0x00, b);

            MemoryStream streamNull = null;
            Assert.IsTrue(AreExceptionsEqual(() => streamNull.Seek(0, SeekOrigin.End), () => streamNull.SkipToEnd()));
        }
    }
}