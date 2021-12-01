//#define LoopTest
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLibs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MyLibs.Tests
{
    [TestClass]
    public class NumberFormattingTests
    {
        [TestMethod]
        public void GetSimplifiedPowerTest()
        {
            var a = NumberFormatting.GetSimplifiedPower(3 * 1);
            var b = NumberFormatting.GetSimplifiedPower(3 * 2);
            var c = NumberFormatting.GetSimplifiedPower(3 * 3);
            var z = NumberFormatting.GetSimplifiedPower(3 * 26);
            var aa = NumberFormatting.GetSimplifiedPower(3 * 26 + (3 * 1));
            var ab = NumberFormatting.GetSimplifiedPower(3 * 26 + (3 * 2));
            var ac = NumberFormatting.GetSimplifiedPower(3 * 26 + (3 * 3));
            var az = NumberFormatting.GetSimplifiedPower(3 * 26 + (3 * 26));
            var ba = NumberFormatting.GetSimplifiedPower(3 * 26 + (3 * 26) + (3 * 1));
            var bb = NumberFormatting.GetSimplifiedPower(3 * 26 + (3 * 26) + (3 * 2));
            var bc = NumberFormatting.GetSimplifiedPower(3 * 26 + (3 * 26) + (3 * 3));
            var bz = NumberFormatting.GetSimplifiedPower(3 * 26 + (3 * 26) + (3 * 26));
            var zz = NumberFormatting.GetSimplifiedPower(3 * 26 * 26 + (3 * 26));
            var aaa = NumberFormatting.GetSimplifiedPower(3 * 26 * 26 + (3 * 26) + (3 * 1));

            Assert.AreEqual("a", a);
            Assert.AreEqual("b", b);
            Assert.AreEqual("c", c);
            Assert.AreEqual("z", z);
            Assert.AreEqual("aa", aa);
            Assert.AreEqual("ab", ab);
            Assert.AreEqual("ac", ac);
            Assert.AreEqual("az", az);
            Assert.AreEqual("ba", ba);
            Assert.AreEqual("bb", bb);
            Assert.AreEqual("bc", bc);
            Assert.AreEqual("bz", bz);
            Assert.AreEqual("zz", zz);
            Assert.AreEqual("aaa", aaa);
        }

        [TestMethod]
        public void ToSimplifiedNotationLongTest()
        {
            // Numbers below 1000 should be printed normally
            for (long i = 0; i <= 999; i++)
            {
                Assert.AreEqual(i.ToString(), NumberFormatting.ToSimplifiedNotation(i));
            }

            Assert.AreEqual("1a", NumberFormatting.ToSimplifiedNotation(1000, 0));
            Assert.AreEqual("1.0a", NumberFormatting.ToSimplifiedNotation(1000, 1));
            Assert.AreEqual("1.00a", NumberFormatting.ToSimplifiedNotation(1000));
            Assert.AreEqual("1.000a", NumberFormatting.ToSimplifiedNotation(1000, 3));
            Assert.AreEqual("1.0000a", NumberFormatting.ToSimplifiedNotation(1000, 4));

            #if LoopTest
            // Test 1000 to 9999
            for (long i = 001_000; i <= 999_999; i++)
            {
                var splitIndex = i.ToString().Length - 3;
                var major = i.ToString().Substring(0, splitIndex);
                var minor = i.ToString().Substring(splitIndex, 2);

                Assert.AreEqual($"{major}.{minor}a", NumberFormatting.ToSimplifiedNotation(i));
            }

            // Test 001_000_000 to 999_999_999
            for (long i = 001_000_000; i <= 999_999_999; i += 1000)
            {
                var splitIndex = i.ToString().Length - 6;
                var major = i.ToString().Substring(0, splitIndex);
                var minor = i.ToString().Substring(splitIndex, 2);

                Assert.AreEqual($"{major}.{minor}b", NumberFormatting.ToSimplifiedNotation(i));
            }

            // Test 001_000_000 to 999_999_999
            for (long i = 001_000_000_000; i <= 999_999_999_999; i += 1000000)
            {
                var splitIndex = i.ToString().Length - 9;
                var major = i.ToString().Substring(0, splitIndex);
                var minor = i.ToString().Substring(splitIndex, 2);

                Assert.AreEqual($"{major}.{minor}c", NumberFormatting.ToSimplifiedNotation(i));
            }
            #endif

            const long a = -123_456;
            Assert.AreEqual("-123a", NumberFormatting.ToSimplifiedNotation(a, 0));
            Assert.AreEqual("-123.4a", NumberFormatting.ToSimplifiedNotation(a, 1));
            Assert.AreEqual("-123.45a", NumberFormatting.ToSimplifiedNotation(a));
            Assert.AreEqual("-123.456a", NumberFormatting.ToSimplifiedNotation(a, 3));
            Assert.AreEqual("-123.4560a", NumberFormatting.ToSimplifiedNotation(a, 4));

            const long b = -123_456_789;
            Assert.AreEqual("-123b", NumberFormatting.ToSimplifiedNotation(b, 0));
            Assert.AreEqual("-123.4b", NumberFormatting.ToSimplifiedNotation(b, 1));
            Assert.AreEqual("-123.45b", NumberFormatting.ToSimplifiedNotation(b));
            Assert.AreEqual("-123.456b", NumberFormatting.ToSimplifiedNotation(b, 3));
            Assert.AreEqual("-123.4567b", NumberFormatting.ToSimplifiedNotation(b, 4));
            Assert.AreEqual("-123.45678b", NumberFormatting.ToSimplifiedNotation(b, 5));
            Assert.AreEqual("-123.456789b", NumberFormatting.ToSimplifiedNotation(b, 6));
            Assert.AreEqual("-123.4567890b", NumberFormatting.ToSimplifiedNotation(b, 7));

            const long c = -123_456_789_876;
            Assert.AreEqual("-123c", NumberFormatting.ToSimplifiedNotation(c, 0));
            Assert.AreEqual("-123.4c", NumberFormatting.ToSimplifiedNotation(c, 1));
            Assert.AreEqual("-123.45c", NumberFormatting.ToSimplifiedNotation(c));
            Assert.AreEqual("-123.456c", NumberFormatting.ToSimplifiedNotation(c, 3));
            Assert.AreEqual("-123.4567c", NumberFormatting.ToSimplifiedNotation(c, 4));
            Assert.AreEqual("-123.45678c", NumberFormatting.ToSimplifiedNotation(c, 5));
            Assert.AreEqual("-123.456789c", NumberFormatting.ToSimplifiedNotation(c, 6));
            Assert.AreEqual("-123.4567898c", NumberFormatting.ToSimplifiedNotation(c, 7));
            Assert.AreEqual("-123.45678987c", NumberFormatting.ToSimplifiedNotation(c, 8));
            Assert.AreEqual("-123.456789876c", NumberFormatting.ToSimplifiedNotation(c, 9));
            Assert.AreEqual("-123.4567898760c", NumberFormatting.ToSimplifiedNotation(c, 10));
        }

        [TestMethod]
        public void ToSimplifiedNotationULongTest()
        {
            // Numbers below 1000 should be printed normally
            for (ulong i = 0; i <= 999; i++)
            {
                Assert.AreEqual(i.ToString(), NumberFormatting.ToSimplifiedNotation(i));
            }

            Assert.AreEqual("1a", NumberFormatting.ToSimplifiedNotation(1000UL, 0));
            Assert.AreEqual("1.0a", NumberFormatting.ToSimplifiedNotation(1000UL, 1));
            Assert.AreEqual("1.00a", NumberFormatting.ToSimplifiedNotation(1000UL));
            Assert.AreEqual("1.000a", NumberFormatting.ToSimplifiedNotation(1000UL, 3));
            Assert.AreEqual("1.0000a", NumberFormatting.ToSimplifiedNotation(1000UL, 4));

            #if LoopTest
            // Test 1000 to 9999
            for (ulong i = 001_000; i <= 999_999; i++)
            {
                var splitIndex = i.ToString().Length - 3;
                var major = i.ToString().Substring(0, splitIndex);
                var minor = i.ToString().Substring(splitIndex, 2);

                Assert.AreEqual($"{major}.{minor}a", NumberFormatting.ToSimplifiedNotation(i));
            }

            // Test 001_000_000 to 999_999_999
            for (ulong i = 001_000_000; i <= 999_999_999; i += 1000)
            {
                var splitIndex = i.ToString().Length - 6;
                var major = i.ToString().Substring(0, splitIndex);
                var minor = i.ToString().Substring(splitIndex, 2);

                Assert.AreEqual($"{major}.{minor}b", NumberFormatting.ToSimplifiedNotation(i));
            }

            // Test 001_000_000 to 999_999_999
            for (ulong i = 001_000_000_000; i <= 999_999_999_999; i += 1000000)
            {
                var splitIndex = i.ToString().Length - 9;
                var major = i.ToString().Substring(0, splitIndex);
                var minor = i.ToString().Substring(splitIndex, 2);

                Assert.AreEqual($"{major}.{minor}c", NumberFormatting.ToSimplifiedNotation(i));
            }
            #endif

            const ulong a = 123_456;
            Assert.AreEqual("123a", NumberFormatting.ToSimplifiedNotation(a, 0));
            Assert.AreEqual("123.4a", NumberFormatting.ToSimplifiedNotation(a, 1));
            Assert.AreEqual("123.45a", NumberFormatting.ToSimplifiedNotation(a));
            Assert.AreEqual("123.456a", NumberFormatting.ToSimplifiedNotation(a, 3));
            Assert.AreEqual("123.4560a", NumberFormatting.ToSimplifiedNotation(a, 4));

            const ulong b = 123_456_789;
            Assert.AreEqual("123b", NumberFormatting.ToSimplifiedNotation(b, 0));
            Assert.AreEqual("123.4b", NumberFormatting.ToSimplifiedNotation(b, 1));
            Assert.AreEqual("123.45b", NumberFormatting.ToSimplifiedNotation(b));
            Assert.AreEqual("123.456b", NumberFormatting.ToSimplifiedNotation(b, 3));
            Assert.AreEqual("123.4567b", NumberFormatting.ToSimplifiedNotation(b, 4));
            Assert.AreEqual("123.45678b", NumberFormatting.ToSimplifiedNotation(b, 5));
            Assert.AreEqual("123.456789b", NumberFormatting.ToSimplifiedNotation(b, 6));
            Assert.AreEqual("123.4567890b", NumberFormatting.ToSimplifiedNotation(b, 7));

            const ulong c = 123_456_789_876;
            Assert.AreEqual("123c", NumberFormatting.ToSimplifiedNotation(c, 0));
            Assert.AreEqual("123.4c", NumberFormatting.ToSimplifiedNotation(c, 1));
            Assert.AreEqual("123.45c", NumberFormatting.ToSimplifiedNotation(c));
            Assert.AreEqual("123.456c", NumberFormatting.ToSimplifiedNotation(c, 3));
            Assert.AreEqual("123.4567c", NumberFormatting.ToSimplifiedNotation(c, 4));
            Assert.AreEqual("123.45678c", NumberFormatting.ToSimplifiedNotation(c, 5));
            Assert.AreEqual("123.456789c", NumberFormatting.ToSimplifiedNotation(c, 6));
            Assert.AreEqual("123.4567898c", NumberFormatting.ToSimplifiedNotation(c, 7));
            Assert.AreEqual("123.45678987c", NumberFormatting.ToSimplifiedNotation(c, 8));
            Assert.AreEqual("123.456789876c", NumberFormatting.ToSimplifiedNotation(c, 9));
            Assert.AreEqual("123.4567898760c", NumberFormatting.ToSimplifiedNotation(c, 10));
        }

        [TestMethod]
        public void ToSimplifiedNotationBigIntegerTest()
        {
            // Numbers below 1000 should be printed normally
            for (BigInteger i = 0; i <= 999; i++)
            {
                Assert.AreEqual(i.ToString(), NumberFormatting.ToSimplifiedNotation(i));
            }

            Assert.AreEqual("1a", NumberFormatting.ToSimplifiedNotation(new BigInteger(1000), 0));
            Assert.AreEqual("1.0a", NumberFormatting.ToSimplifiedNotation(new BigInteger(1000), 1));
            Assert.AreEqual("1.00a", NumberFormatting.ToSimplifiedNotation(new BigInteger(1000)));
            Assert.AreEqual("1.000a", NumberFormatting.ToSimplifiedNotation(new BigInteger(1000), 3));
            Assert.AreEqual("1.0000a", NumberFormatting.ToSimplifiedNotation(new BigInteger(1000), 4));

            #if LoopTest
            // Test 1000 to 9999
            for (BigInteger i = 001_000; i <= 999_999; i++)
            {
                var splitIndex = i.ToString().Length - 3;
                var major = i.ToString().Substring(0, splitIndex);
                var minor = i.ToString().Substring(splitIndex, 2);

                Assert.AreEqual($"{major}.{minor}a", NumberFormatting.ToSimplifiedNotation(i));
            }

            // Test 001_000_000 to 999_999_999
            for (BigInteger i = 001_000_000; i <= 999_999_999; i += 1000)
            {
                var splitIndex = i.ToString().Length - 6;
                var major = i.ToString().Substring(0, splitIndex);
                var minor = i.ToString().Substring(splitIndex, 2);

                Assert.AreEqual($"{major}.{minor}b", NumberFormatting.ToSimplifiedNotation(i));
            }

            // Test 001_000_000 to 999_999_999
            for (BigInteger i = 001_000_000_000; i <= 999_999_999_999; i += 1000000)
            {
                var splitIndex = i.ToString().Length - 9;
                var major = i.ToString().Substring(0, splitIndex);
                var minor = i.ToString().Substring(splitIndex, 2);

                Assert.AreEqual($"{major}.{minor}c", NumberFormatting.ToSimplifiedNotation(i));
            }
            #endif

            BigInteger a = -123_456;
            Assert.AreEqual("-123a", NumberFormatting.ToSimplifiedNotation(a, 0));
            Assert.AreEqual("-123.4a", NumberFormatting.ToSimplifiedNotation(a, 1));
            Assert.AreEqual("-123.45a", NumberFormatting.ToSimplifiedNotation(a));
            Assert.AreEqual("-123.456a", NumberFormatting.ToSimplifiedNotation(a, 3));
            Assert.AreEqual("-123.4560a", NumberFormatting.ToSimplifiedNotation(a, 4));

            BigInteger b = -123_456_789;
            Assert.AreEqual("-123b", NumberFormatting.ToSimplifiedNotation(b, 0));
            Assert.AreEqual("-123.4b", NumberFormatting.ToSimplifiedNotation(b, 1));
            Assert.AreEqual("-123.45b", NumberFormatting.ToSimplifiedNotation(b));
            Assert.AreEqual("-123.456b", NumberFormatting.ToSimplifiedNotation(b, 3));
            Assert.AreEqual("-123.4567b", NumberFormatting.ToSimplifiedNotation(b, 4));
            Assert.AreEqual("-123.45678b", NumberFormatting.ToSimplifiedNotation(b, 5));
            Assert.AreEqual("-123.456789b", NumberFormatting.ToSimplifiedNotation(b, 6));
            Assert.AreEqual("-123.4567890b", NumberFormatting.ToSimplifiedNotation(b, 7));

            BigInteger c = -123_456_789_876;
            Assert.AreEqual("-123c", NumberFormatting.ToSimplifiedNotation(c, 0));
            Assert.AreEqual("-123.4c", NumberFormatting.ToSimplifiedNotation(c, 1));
            Assert.AreEqual("-123.45c", NumberFormatting.ToSimplifiedNotation(c));
            Assert.AreEqual("-123.456c", NumberFormatting.ToSimplifiedNotation(c, 3));
            Assert.AreEqual("-123.4567c", NumberFormatting.ToSimplifiedNotation(c, 4));
            Assert.AreEqual("-123.45678c", NumberFormatting.ToSimplifiedNotation(c, 5));
            Assert.AreEqual("-123.456789c", NumberFormatting.ToSimplifiedNotation(c, 6));
            Assert.AreEqual("-123.4567898c", NumberFormatting.ToSimplifiedNotation(c, 7));
            Assert.AreEqual("-123.45678987c", NumberFormatting.ToSimplifiedNotation(c, 8));
            Assert.AreEqual("-123.456789876c", NumberFormatting.ToSimplifiedNotation(c, 9));
            Assert.AreEqual("-123.4567898760c", NumberFormatting.ToSimplifiedNotation(c, 10));
        }

        [TestMethod]
        public void ToScientificNotationLongTest()
        {
            Assert.AreEqual("-9.99e+2", NumberFormatting.ToScientificNotation(-999, 2));
            Assert.AreEqual("-1.000e+3", NumberFormatting.ToScientificNotation(-1000, 3));
            Assert.AreEqual("-1.001e+3", NumberFormatting.ToScientificNotation(-1001, 3));
            Assert.AreEqual("-9.999e+3", NumberFormatting.ToScientificNotation(-9999, 3));
            Assert.AreEqual("-1.0000e+4", NumberFormatting.ToScientificNotation(-10000, 4));
            Assert.AreEqual("-1.0001e+4", NumberFormatting.ToScientificNotation(-10001, 4));

            const long a = -123_456;
            Assert.AreEqual("-1e+5", NumberFormatting.ToScientificNotation(a, 0));
            Assert.AreEqual("-1.2e+5", NumberFormatting.ToScientificNotation(a, 1));
            Assert.AreEqual("-1.23e+5", NumberFormatting.ToScientificNotation(a));
            Assert.AreEqual("-1.234e+5", NumberFormatting.ToScientificNotation(a, 3));
            Assert.AreEqual("-1.2345e+5", NumberFormatting.ToScientificNotation(a, 4));
            Assert.AreEqual("-1.23456e+5", NumberFormatting.ToScientificNotation(a, 5));
            Assert.AreEqual("-123456", NumberFormatting.ToScientificNotation(a, 6));

            const long b = -123_456_789;
            Assert.AreEqual("-1e+8", NumberFormatting.ToScientificNotation(b, 0));
            Assert.AreEqual("-1.2e+8", NumberFormatting.ToScientificNotation(b, 1));
            Assert.AreEqual("-1.23e+8", NumberFormatting.ToScientificNotation(b));
            Assert.AreEqual("-1.234e+8", NumberFormatting.ToScientificNotation(b, 3));
            Assert.AreEqual("-1.2345e+8", NumberFormatting.ToScientificNotation(b, 4));
            Assert.AreEqual("-1.23456e+8", NumberFormatting.ToScientificNotation(b, 5));
            Assert.AreEqual("-1.234567e+8", NumberFormatting.ToScientificNotation(b, 6));
            Assert.AreEqual("-1.2345678e+8", NumberFormatting.ToScientificNotation(b, 7));
            Assert.AreEqual("-1.23456789e+8", NumberFormatting.ToScientificNotation(b, 8));
            Assert.AreEqual("-123456789", NumberFormatting.ToScientificNotation(b, 9));

            const long c = -123_456_789_876;
            Assert.AreEqual("-1e+11", NumberFormatting.ToScientificNotation(c, 0));
            Assert.AreEqual("-1.2e+11", NumberFormatting.ToScientificNotation(c, 1));
            Assert.AreEqual("-1.23e+11", NumberFormatting.ToScientificNotation(c));
            Assert.AreEqual("-1.234e+11", NumberFormatting.ToScientificNotation(c, 3));
            Assert.AreEqual("-1.2345e+11", NumberFormatting.ToScientificNotation(c, 4));
            Assert.AreEqual("-1.23456e+11", NumberFormatting.ToScientificNotation(c, 5));
            Assert.AreEqual("-1.234567e+11", NumberFormatting.ToScientificNotation(c, 6));
            Assert.AreEqual("-1.2345678e+11", NumberFormatting.ToScientificNotation(c, 7));
            Assert.AreEqual("-1.23456789e+11", NumberFormatting.ToScientificNotation(c, 8));
            Assert.AreEqual("-1.234567898e+11", NumberFormatting.ToScientificNotation(c, 9));
            Assert.AreEqual("-1.2345678987e+11", NumberFormatting.ToScientificNotation(c, 10));
            Assert.AreEqual("-1.23456789876e+11", NumberFormatting.ToScientificNotation(c, 11));
            Assert.AreEqual("-123456789876", NumberFormatting.ToScientificNotation(c, 12));
        }

        [TestMethod]
        public void ToScientificNotationULongTest()
        {
            Assert.AreEqual("9.99e+2", NumberFormatting.ToScientificNotation(999UL, 2));
            Assert.AreEqual("1.000e+3", NumberFormatting.ToScientificNotation(1000UL, 3));
            Assert.AreEqual("1.001e+3", NumberFormatting.ToScientificNotation(1001UL, 3));
            Assert.AreEqual("9.999e+3", NumberFormatting.ToScientificNotation(9999UL, 3));
            Assert.AreEqual("1.0000e+4", NumberFormatting.ToScientificNotation(10000UL, 4));
            Assert.AreEqual("1.0001e+4", NumberFormatting.ToScientificNotation(10001UL, 4));

            const ulong a = 123_456;
            Assert.AreEqual("1e+5", NumberFormatting.ToScientificNotation(a, 0));
            Assert.AreEqual("1.2e+5", NumberFormatting.ToScientificNotation(a, 1));
            Assert.AreEqual("1.23e+5", NumberFormatting.ToScientificNotation(a));
            Assert.AreEqual("1.234e+5", NumberFormatting.ToScientificNotation(a, 3));
            Assert.AreEqual("1.2345e+5", NumberFormatting.ToScientificNotation(a, 4));
            Assert.AreEqual("1.23456e+5", NumberFormatting.ToScientificNotation(a, 5));
            Assert.AreEqual("123456", NumberFormatting.ToScientificNotation(a, 6));

            const ulong b = 123_456_789;
            Assert.AreEqual("1e+8", NumberFormatting.ToScientificNotation(b, 0));
            Assert.AreEqual("1.2e+8", NumberFormatting.ToScientificNotation(b, 1));
            Assert.AreEqual("1.23e+8", NumberFormatting.ToScientificNotation(b));
            Assert.AreEqual("1.234e+8", NumberFormatting.ToScientificNotation(b, 3));
            Assert.AreEqual("1.2345e+8", NumberFormatting.ToScientificNotation(b, 4));
            Assert.AreEqual("1.23456e+8", NumberFormatting.ToScientificNotation(b, 5));
            Assert.AreEqual("1.234567e+8", NumberFormatting.ToScientificNotation(b, 6));
            Assert.AreEqual("1.2345678e+8", NumberFormatting.ToScientificNotation(b, 7));
            Assert.AreEqual("1.23456789e+8", NumberFormatting.ToScientificNotation(b, 8));
            Assert.AreEqual("123456789", NumberFormatting.ToScientificNotation(b, 9));

            const ulong c = 123_456_789_876;
            Assert.AreEqual("1e+11", NumberFormatting.ToScientificNotation(c, 0));
            Assert.AreEqual("1.2e+11", NumberFormatting.ToScientificNotation(c, 1));
            Assert.AreEqual("1.23e+11", NumberFormatting.ToScientificNotation(c));
            Assert.AreEqual("1.234e+11", NumberFormatting.ToScientificNotation(c, 3));
            Assert.AreEqual("1.2345e+11", NumberFormatting.ToScientificNotation(c, 4));
            Assert.AreEqual("1.23456e+11", NumberFormatting.ToScientificNotation(c, 5));
            Assert.AreEqual("1.234567e+11", NumberFormatting.ToScientificNotation(c, 6));
            Assert.AreEqual("1.2345678e+11", NumberFormatting.ToScientificNotation(c, 7));
            Assert.AreEqual("1.23456789e+11", NumberFormatting.ToScientificNotation(c, 8));
            Assert.AreEqual("1.234567898e+11", NumberFormatting.ToScientificNotation(c, 9));
            Assert.AreEqual("1.2345678987e+11", NumberFormatting.ToScientificNotation(c, 10));
            Assert.AreEqual("1.23456789876e+11", NumberFormatting.ToScientificNotation(c, 11));
            Assert.AreEqual("123456789876", NumberFormatting.ToScientificNotation(c, 12));
        }

        [TestMethod]
        public void ToScientificNotationBigIntegerTest()
        {
            Assert.AreEqual("-9.99e+2", NumberFormatting.ToScientificNotation(new BigInteger(-999), 2));
            Assert.AreEqual("-1.000e+3", NumberFormatting.ToScientificNotation(new BigInteger(-1000), 3));
            Assert.AreEqual("-1.001e+3", NumberFormatting.ToScientificNotation(new BigInteger(-1001), 3));
            Assert.AreEqual("-9.999e+3", NumberFormatting.ToScientificNotation(new BigInteger(-9999), 3));
            Assert.AreEqual("-1.0000e+4", NumberFormatting.ToScientificNotation(new BigInteger(-10000), 4));
            Assert.AreEqual("-1.0001e+4", NumberFormatting.ToScientificNotation(new BigInteger(-10001), 4));

            BigInteger a = -123_456;
            Assert.AreEqual("-1e+5", NumberFormatting.ToScientificNotation(a, 0));
            Assert.AreEqual("-1.2e+5", NumberFormatting.ToScientificNotation(a, 1));
            Assert.AreEqual("-1.23e+5", NumberFormatting.ToScientificNotation(a));
            Assert.AreEqual("-1.234e+5", NumberFormatting.ToScientificNotation(a, 3));
            Assert.AreEqual("-1.2345e+5", NumberFormatting.ToScientificNotation(a, 4));
            Assert.AreEqual("-1.23456e+5", NumberFormatting.ToScientificNotation(a, 5));
            Assert.AreEqual("-123456", NumberFormatting.ToScientificNotation(a, 6));

            BigInteger b = -123_456_789;
            Assert.AreEqual("-1e+8", NumberFormatting.ToScientificNotation(b, 0));
            Assert.AreEqual("-1.2e+8", NumberFormatting.ToScientificNotation(b, 1));
            Assert.AreEqual("-1.23e+8", NumberFormatting.ToScientificNotation(b));
            Assert.AreEqual("-1.234e+8", NumberFormatting.ToScientificNotation(b, 3));
            Assert.AreEqual("-1.2345e+8", NumberFormatting.ToScientificNotation(b, 4));
            Assert.AreEqual("-1.23456e+8", NumberFormatting.ToScientificNotation(b, 5));
            Assert.AreEqual("-1.234567e+8", NumberFormatting.ToScientificNotation(b, 6));
            Assert.AreEqual("-1.2345678e+8", NumberFormatting.ToScientificNotation(b, 7));
            Assert.AreEqual("-1.23456789e+8", NumberFormatting.ToScientificNotation(b, 8));
            Assert.AreEqual("-123456789", NumberFormatting.ToScientificNotation(b, 9));

            BigInteger c = -123_456_789_876;
            Assert.AreEqual("-1e+11", NumberFormatting.ToScientificNotation(c, 0));
            Assert.AreEqual("-1.2e+11", NumberFormatting.ToScientificNotation(c, 1));
            Assert.AreEqual("-1.23e+11", NumberFormatting.ToScientificNotation(c));
            Assert.AreEqual("-1.234e+11", NumberFormatting.ToScientificNotation(c, 3));
            Assert.AreEqual("-1.2345e+11", NumberFormatting.ToScientificNotation(c, 4));
            Assert.AreEqual("-1.23456e+11", NumberFormatting.ToScientificNotation(c, 5));
            Assert.AreEqual("-1.234567e+11", NumberFormatting.ToScientificNotation(c, 6));
            Assert.AreEqual("-1.2345678e+11", NumberFormatting.ToScientificNotation(c, 7));
            Assert.AreEqual("-1.23456789e+11", NumberFormatting.ToScientificNotation(c, 8));
            Assert.AreEqual("-1.234567898e+11", NumberFormatting.ToScientificNotation(c, 9));
            Assert.AreEqual("-1.2345678987e+11", NumberFormatting.ToScientificNotation(c, 10));
            Assert.AreEqual("-1.23456789876e+11", NumberFormatting.ToScientificNotation(c, 11));
            Assert.AreEqual("-123456789876", NumberFormatting.ToScientificNotation(c, 12));
        }
    }
}