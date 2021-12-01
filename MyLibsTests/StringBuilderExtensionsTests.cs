using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLibs;
using System;
using System.Collections.Generic;
using System.Text;
using static MyLibs.Tests.ExceptionUtilities;

namespace MyLibs.Tests
{
    [TestClass]
    public class StringBuilderExtensionsTests
    {
        private const string trimTest = "   ,.?TEST?.,   ";

        [TestMethod]
        public void TrimTest()
        {
            var sb = new StringBuilder(trimTest);

            Assert.AreEqual(trimTest.Trim(), sb.Trim().ToString());

            sb.Clear();
            sb.Append(trimTest);

            Assert.AreEqual(trimTest.Trim(' '), sb.Trim(' ').ToString());

            sb.Clear();
            sb.Append(trimTest);

            Assert.AreEqual(trimTest.Trim(' ', ','), sb.Trim(' ', ',').ToString());

            sb.Clear();
            sb.Append(trimTest);

            Assert.AreEqual(trimTest.Trim(' ', ',', '.'), sb.Trim(' ', ',', '.').ToString());

            sb.Clear();
            sb.Append(trimTest);

            Assert.AreEqual(trimTest.Trim(' ', ',', '.', '?'), sb.Trim(' ', ',', '.', '?').ToString());

            sb.Clear();
            sb.Append(trimTest);

            Assert.AreEqual(trimTest.Trim(null), sb.Trim(null).ToString());
            Assert.AreEqual(trimTest.Trim(Array.Empty<char>()), sb.Trim(Array.Empty<char>()).ToString());

            string nullString = null;
            StringBuilder nullSb = null;

            Assert.IsTrue(AreExceptionsEqual(() => nullString.Trim(), () => nullSb.Trim()));
            Assert.IsTrue(AreExceptionsEqual(() => nullString.Trim(' ', ',', '.', '?'), () => nullSb.Trim(' ', ',', '.', '?')));
        }

        [TestMethod]
        public void TrimStartTest()
        {
            var sb = new StringBuilder(trimTest);

            Assert.AreEqual(trimTest.TrimStart(), sb.TrimStart().ToString());

            sb.Clear();
            sb.Append(trimTest);

            Assert.AreEqual(trimTest.TrimStart(' '), sb.TrimStart(' ').ToString());

            sb.Clear();
            sb.Append(trimTest);

            Assert.AreEqual(trimTest.TrimStart(' ', ','), sb.TrimStart(' ', ',').ToString());

            sb.Clear();
            sb.Append(trimTest);

            Assert.AreEqual(trimTest.TrimStart(' ', ',', '.'), sb.TrimStart(' ', ',', '.').ToString());

            sb.Clear();
            sb.Append(trimTest);

            Assert.AreEqual(trimTest.TrimStart(' ', ',', '.', '?'), sb.TrimStart(' ', ',', '.', '?').ToString());

            sb.Clear();
            sb.Append(trimTest);

            Assert.AreEqual(trimTest.TrimStart(null), sb.TrimStart(null).ToString());
            Assert.AreEqual(trimTest.TrimStart(Array.Empty<char>()), sb.TrimStart(Array.Empty<char>()).ToString());

            string nullString = null;
            StringBuilder nullSb = null;

            Assert.IsTrue(AreExceptionsEqual(() => nullString.TrimStart(), () => nullSb.TrimStart()));
            Assert.IsTrue(AreExceptionsEqual(() => nullString.TrimStart(' ', ',', '.', '?'), () => nullSb.TrimStart(' ', ',', '.', '?')));
        }

        [TestMethod]
        public void TrimEndTest()
        {
            var sb = new StringBuilder(trimTest);

            Assert.AreEqual(trimTest.TrimEnd(), sb.TrimEnd().ToString());

            sb.Clear();
            sb.Append(trimTest);

            Assert.AreEqual(trimTest.TrimEnd(' '), sb.TrimEnd(' ').ToString());

            sb.Clear();
            sb.Append(trimTest);

            Assert.AreEqual(trimTest.TrimEnd(' ', ','), sb.TrimEnd(' ', ',').ToString());

            sb.Clear();
            sb.Append(trimTest);

            Assert.AreEqual(trimTest.TrimEnd(' ', ',', '.'), sb.TrimEnd(' ', ',', '.').ToString());

            sb.Clear();
            sb.Append(trimTest);

            Assert.AreEqual(trimTest.TrimEnd(' ', ',', '.', '?'), sb.TrimEnd(' ', ',', '.', '?').ToString());

            sb.Clear();
            sb.Append(trimTest);

            Assert.AreEqual(trimTest.TrimEnd(null), sb.TrimEnd(null).ToString());
            Assert.AreEqual(trimTest.TrimEnd(Array.Empty<char>()), sb.TrimEnd(Array.Empty<char>()).ToString());

            string nullString = null;
            StringBuilder nullSb = null;

            Assert.IsTrue(AreExceptionsEqual(() => nullString.TrimEnd(), () => nullSb.TrimEnd()));
            Assert.IsTrue(AreExceptionsEqual(() => nullString.TrimEnd(' ', ',', '.', '?'), () => nullSb.TrimEnd(' ', ',', '.', '?')));
        }

        private static bool Contains(StringBuilder sb, string str, string search)
        {
            return sb.Contains(search) && sb.Contains(search) == str.Contains(search);
        }

        private static bool NotContains(StringBuilder sb, string str, string search)
        {
            return !sb.Contains(search) && sb.Contains(search) == str.Contains(search);
        }

        private static bool ContainsIgnoreCase(StringBuilder sb, string str, string search)
        {
            return sb.Contains(search, StringComparison.OrdinalIgnoreCase) && sb.Contains(search, StringComparison.OrdinalIgnoreCase) == str.Contains(search, StringComparison.OrdinalIgnoreCase);
        }

        private static bool NotContainsIgnoreCase(StringBuilder sb, string str, string search)
        {
            return !sb.Contains(search, StringComparison.OrdinalIgnoreCase) && sb.Contains(search, StringComparison.OrdinalIgnoreCase) == str.Contains(search, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IndexOf(StringBuilder sb, string str, string search, int index)
        {
            return sb.IndexOf(search) == index && sb.IndexOf(search) == str.IndexOf(search);
        }

        private static bool IndexOfIgnoreCase(StringBuilder sb, string str, string search, int index)
        {
            return sb.IndexOf(search, StringComparison.OrdinalIgnoreCase) == index && sb.IndexOf(search, StringComparison.OrdinalIgnoreCase) == str.IndexOf(search, StringComparison.OrdinalIgnoreCase);
        }

        [TestMethod]
        public void ContainsTest()
        {
            const string test = "TEST";
            var sb = new StringBuilder("TEST");

            // True
            Assert.IsTrue(Contains(sb, test, ""));
            Assert.IsTrue(Contains(sb, test, "T"));
            Assert.IsTrue(Contains(sb, test, "E"));
            Assert.IsTrue(Contains(sb, test, "S"));
            Assert.IsTrue(Contains(sb, test, "TE"));
            Assert.IsTrue(Contains(sb, test, "TES"));
            Assert.IsTrue(Contains(sb, test, "TEST"));

            // False
            Assert.IsTrue(NotContains(sb, test, "X"));
            Assert.IsTrue(NotContains(sb, test, "XX"));
            Assert.IsTrue(NotContains(sb, test, "XXX"));
            Assert.IsTrue(NotContains(sb, test, "XXXX"));
            Assert.IsTrue(NotContains(sb, test, "XXXXX"));
            Assert.IsTrue(NotContains(sb, test, "XXXXXXXXXX"));
            Assert.IsTrue(NotContains(sb, test, "XT"));
            Assert.IsTrue(NotContains(sb, test, "XTE"));
            Assert.IsTrue(NotContains(sb, test, "XTES"));
            Assert.IsTrue(NotContains(sb, test, "XTEST"));
            Assert.IsTrue(NotContains(sb, test, "XTESTX"));
            Assert.IsTrue(NotContains(sb, test, "TESTX"));
            Assert.IsTrue(NotContains(sb, test, "TESX"));
            Assert.IsTrue(NotContains(sb, test, "TEX"));
            Assert.IsTrue(NotContains(sb, test, "TX"));

            // Exception
            Assert.IsTrue(AreExceptionsEqual(() => test.Contains(null), () => sb.Contains(null)));

            string stringNull = null;
            StringBuilder sbNull = null;

            Assert.IsTrue(AreExceptionsEqual(() => stringNull.Contains("TEST"), () => sbNull.Contains("NULL")));
        }

        [TestMethod]
        public void IndexOfTest()
        {
            const string test = "TEST";
            var sb = new StringBuilder("TEST");

            // Found
            Assert.IsTrue(IndexOf(sb, test, "", 0));
            Assert.IsTrue(IndexOf(sb, test, "T", 0));
            Assert.IsTrue(IndexOf(sb, test, "E", 1));
            Assert.IsTrue(IndexOf(sb, test, "S", 2));
            Assert.IsTrue(IndexOf(sb, test, "TE", 0));
            Assert.IsTrue(IndexOf(sb, test, "TES", 0));
            Assert.IsTrue(IndexOf(sb, test, "TEST", 0));

            // Not Found
            Assert.IsTrue(IndexOf(sb, test, "X", -1));
            Assert.IsTrue(IndexOf(sb, test, "XX", -1));
            Assert.IsTrue(IndexOf(sb, test, "XXX", -1));
            Assert.IsTrue(IndexOf(sb, test, "XXXX", -1));
            Assert.IsTrue(IndexOf(sb, test, "XXXXX", -1));
            Assert.IsTrue(IndexOf(sb, test, "XT", -1));
            Assert.IsTrue(IndexOf(sb, test, "XTE", -1));
            Assert.IsTrue(IndexOf(sb, test, "XTES", -1));
            Assert.IsTrue(IndexOf(sb, test, "XTEST", -1));
            Assert.IsTrue(IndexOf(sb, test, "XTESTX", -1));
            Assert.IsTrue(IndexOf(sb, test, "TESTX", -1));
            Assert.IsTrue(IndexOf(sb, test, "TESX", -1));
            Assert.IsTrue(IndexOf(sb, test, "TEX", -1));
            Assert.IsTrue(IndexOf(sb, test, "TEX", -1));
            Assert.IsTrue(IndexOf(sb, test, "TX", -1));

            // Exception
            Assert.IsTrue(AreExceptionsEqual(() => test.IndexOf(null), () => sb.IndexOf(null)));

            string stringNull = null;
            StringBuilder sbNull = null;

            Assert.IsTrue(AreExceptionsEqual(() => stringNull.IndexOf("TEST"), () => sbNull.IndexOf("TEST")));
        }

        [TestMethod]
        public void ContainsComparisonTest()
        {
            const string test = "test";
            var sb = new StringBuilder("test");

            // True
            Assert.IsTrue(ContainsIgnoreCase(sb, test, ""));
            Assert.IsTrue(ContainsIgnoreCase(sb, test, "T"));
            Assert.IsTrue(ContainsIgnoreCase(sb, test, "E"));
            Assert.IsTrue(ContainsIgnoreCase(sb, test, "S"));
            Assert.IsTrue(ContainsIgnoreCase(sb, test, "TE"));
            Assert.IsTrue(ContainsIgnoreCase(sb, test, "TES"));
            Assert.IsTrue(ContainsIgnoreCase(sb, test, "TEST"));

            // False
            Assert.IsTrue(NotContainsIgnoreCase(sb, test, "X"));
            Assert.IsTrue(NotContainsIgnoreCase(sb, test, "XX"));
            Assert.IsTrue(NotContainsIgnoreCase(sb, test, "XXX"));
            Assert.IsTrue(NotContainsIgnoreCase(sb, test, "XXXX"));
            Assert.IsTrue(NotContainsIgnoreCase(sb, test, "XXXXX"));
            Assert.IsTrue(NotContainsIgnoreCase(sb, test, "XXXXXXXXXX"));
            Assert.IsTrue(NotContainsIgnoreCase(sb, test, "XT"));
            Assert.IsTrue(NotContainsIgnoreCase(sb, test, "XTE"));
            Assert.IsTrue(NotContainsIgnoreCase(sb, test, "XTES"));
            Assert.IsTrue(NotContainsIgnoreCase(sb, test, "XTEST"));
            Assert.IsTrue(NotContainsIgnoreCase(sb, test, "XTESTX"));
            Assert.IsTrue(NotContainsIgnoreCase(sb, test, "TESTX"));
            Assert.IsTrue(NotContainsIgnoreCase(sb, test, "TESX"));
            Assert.IsTrue(NotContainsIgnoreCase(sb, test, "TEX"));
            Assert.IsTrue(NotContainsIgnoreCase(sb, test, "TX"));

            // Exception
            Assert.IsTrue(AreExceptionsEqual(() => test.Contains(null, StringComparison.OrdinalIgnoreCase), () => sb.Contains(null, StringComparison.OrdinalIgnoreCase)));

            string stringNull = null;
            StringBuilder sbNull = null;

            Assert.IsTrue(AreExceptionsEqual(() => stringNull.Contains("TEST", StringComparison.OrdinalIgnoreCase), () => sbNull.Contains("NULL", StringComparison.OrdinalIgnoreCase)));
        }

        [TestMethod]
        public void IndexOfComparisonTest()
        {
            const string test = "test";
            var sb = new StringBuilder("test");

            // Found
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "", 0));
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "T", 0));
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "E", 1));
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "S", 2));
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "TE", 0));
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "TES", 0));
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "TEST", 0));

            // Not Found
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "X", -1));
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "XX", -1));
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "XXX", -1));
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "XXXX", -1));
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "XXXXX", -1));
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "XT", -1));
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "XTE", -1));
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "XTES", -1));
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "XTEST", -1));
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "XTESTX", -1));
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "TESTX", -1));
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "TESX", -1));
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "TEX", -1));
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "TEX", -1));
            Assert.IsTrue(IndexOfIgnoreCase(sb, test, "TX", -1));

            // Exception
            Assert.IsTrue(AreExceptionsEqual(() => test.IndexOf(null, StringComparison.OrdinalIgnoreCase), () => sb.IndexOf(null, StringComparison.OrdinalIgnoreCase)));

            string stringNull = null;
            StringBuilder sbNull = null;

            Assert.IsTrue(AreExceptionsEqual(() => stringNull.IndexOf("TEST", StringComparison.OrdinalIgnoreCase), () => sbNull.IndexOf("TEST", StringComparison.OrdinalIgnoreCase)));
        }
    }
}