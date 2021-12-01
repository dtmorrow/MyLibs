using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLibs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.IO;

namespace MyLibs.Tests
{
    [TestClass]
    public class LEB128Tests
    {
        [DataRow(0UL)]
        [DataRow(1UL)]
        [DataRow(127UL)]
        [DataRow(128UL)]
        [DataRow(255UL)]
        [DataRow(256UL)]
        [DataRow(ulong.MaxValue)]
        [TestMethod]
        public void UnsignedLEB128Test(ulong value)
        {
            var encode = LEB128.EncodeLEB128(value);
            var decode = LEB128.DecodeUnsignedLEB128(encode);

            Assert.AreEqual(value, decode);
        }

        [DataRow(0L)]
        [DataRow(1L)]
        [DataRow(-1L)]
        [DataRow(127L)]
        [DataRow(-127L)]
        [DataRow(128L)]
        [DataRow(-128L)]
        [DataRow(255L)]
        [DataRow(-255L)]
        [DataRow(256L)]
        [DataRow(-256L)]
        [DataRow(long.MaxValue)]
        [DataRow(long.MinValue)]
        [TestMethod]
        public void SignedLEB128Test(long value)
        {
            var encode = LEB128.EncodeLEB128(value);
            var decode = LEB128.DecodeSignedLEB128(encode);

            Assert.AreEqual(value, decode);
        }

        [TestMethod]
        public void BigLEB128Test()
        {
            var values = new BigInteger[]
            {
                0, 1, -1, 127, -127, 128, -128, 255, -255, 256, -256, long.MaxValue, long.MinValue, long.MaxValue, long.MinValue, long.MaxValue, long.MinValue
            };
            values[^1] -= long.MinValue;
            values[^1] -= long.MinValue;
            values[^2] += long.MaxValue;
            values[^2] += long.MaxValue;
            values[^3] -= long.MinValue;
            values[^4] += long.MaxValue;

            for (int i = 0; i < values.Length; i++)
            {
                var value = values[i];

                var encode = LEB128.EncodeLEB128(value);
                var decode = LEB128.DecodeBigLEB128(encode);

                Assert.AreEqual(value, decode);
            }
        }

        [DataRow(0UL)]
        [DataRow(1UL)]
        [DataRow(127UL)]
        [DataRow(128UL)]
        [DataRow(255UL)]
        [DataRow(256UL)]
        [DataRow(ulong.MaxValue)]
        [TestMethod]
        public void StreamUnsignedLEB128Test(ulong value)
        {
            var stream = new MemoryStream();
            stream.WriteLEB128(value);
            stream.Position = 0;
            var decode = LEB128.ReadUnsignedLEB128(stream);

            Assert.AreEqual(value, decode);
        }

        [DataRow(0L)]
        [DataRow(1L)]
        [DataRow(-1L)]
        [DataRow(127L)]
        [DataRow(-127L)]
        [DataRow(128L)]
        [DataRow(-128L)]
        [DataRow(255L)]
        [DataRow(-255L)]
        [DataRow(256L)]
        [DataRow(-256L)]
        [DataRow(long.MaxValue)]
        [DataRow(long.MinValue)]
        [TestMethod]
        public void StreamSignedLEB128Test(long value)
        {
            var stream = new MemoryStream();
            stream.WriteLEB128(value);
            stream.Position = 0;
            var decode = LEB128.ReadSignedLEB128(stream);

            Assert.AreEqual(value, decode);
        }

        [TestMethod]
        public void StreamBigLEB128Test2()
        {
            var values = new BigInteger[]
            {
                0, 1, -1, 127, -127, 128, -128, 255, -255, 256, -256, long.MaxValue, long.MinValue, long.MaxValue, long.MinValue, long.MaxValue, long.MinValue
            };
            values[^1] -= long.MinValue;
            values[^1] -= long.MinValue;
            values[^2] += long.MaxValue;
            values[^2] += long.MaxValue;
            values[^3] -= long.MinValue;
            values[^4] += long.MaxValue;

            for (int i = 0; i < values.Length; i++)
            {
                var value = values[i];

                var stream = new MemoryStream();
                stream.WriteLEB128(value);
                stream.Position = 0;
                var decode = LEB128.ReadBigLEB128(stream);

                Assert.AreEqual(value, decode);
            }
        }

        [DataRow(ulong.MaxValue, sizeof(ulong))]
        [DataRow(uint.MaxValue + 1UL, sizeof(ulong))]
        [DataRow(uint.MaxValue, sizeof(uint))]
        [DataRow(ushort.MaxValue + 1UL, sizeof(uint))]
        [DataRow(ushort.MaxValue, sizeof(ushort))]
        [DataRow(byte.MaxValue + 1UL, sizeof(ushort))]
        [DataRow(byte.MaxValue, sizeof(byte))]
        [DataRow(0UL, sizeof(byte))]
        [TestMethod]
        public void MinimumByteSizeUnsignedTest(ulong value, int expected)
        {
            Assert.AreEqual(expected, LEB128.MinimumByteSize(value));
        }

        [DataRow(long.MaxValue, sizeof(long))]
        [DataRow(int.MaxValue + 1L, sizeof(long))]
        [DataRow(int.MaxValue, sizeof(int))]
        [DataRow(short.MaxValue + 1L, sizeof(int))]
        [DataRow(short.MaxValue, sizeof(short))]
        [DataRow(sbyte.MaxValue + 1L, sizeof(short))]
        [DataRow(sbyte.MaxValue, sizeof(sbyte))]
        [DataRow(0, sizeof(sbyte))]
        [DataRow(sbyte.MinValue, sizeof(sbyte))]
        [DataRow(sbyte.MinValue - 1L, sizeof(short))]
        [DataRow(short.MinValue, sizeof(short))]
        [DataRow(short.MinValue - 1L, sizeof(int))]
        [DataRow(int.MinValue, sizeof(int))]
        [DataRow(int.MinValue - 1L, sizeof(long))]
        [DataRow(long.MinValue, sizeof(long))]
        [TestMethod]
        public void MinimumByteSizeSignedTest(long value, int expected)
        {
            Assert.AreEqual(expected, LEB128.MinimumByteSize(value));
        }
    }
}