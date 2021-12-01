using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLibs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace MyLibs.Tests
{
    [TestClass]
    public class MiscUtiltiesTests
    {
        [TestMethod]
        public void GeneratePrimesTest()
        {
            var lines = File.ReadLines("primes.txt");
            var primes = MiscUtilties.GeneratePrimes();

            var primeEnumerator = primes.GetEnumerator();
            primeEnumerator.MoveNext();

            foreach (var line in lines)
            {
                Assert.AreEqual(int.Parse(line), primeEnumerator.Current);
                primeEnumerator.MoveNext();
            }
        }

        [TestMethod]
        public void GetPrimeFactorsTest()
        {
            var two = MiscUtilties.GetPrimeFactors(2).ToArray();
            Assert.AreEqual(1, two.Length);
            Assert.AreEqual(2, two[0].Key);
            Assert.AreEqual(1, two[0].Value);

            var one_hundred = MiscUtilties.GetPrimeFactors(100).ToArray();
            Assert.AreEqual(2, one_hundred.Length);
            Assert.AreEqual(2, one_hundred[0].Key);
            Assert.AreEqual(2, one_hundred[0].Value);
            Assert.AreEqual(5, one_hundred[1].Key);
            Assert.AreEqual(2, one_hundred[1].Value);

            var fourty_five_thousand = MiscUtilties.GetPrimeFactors(45000).ToArray();
            Assert.AreEqual(3, fourty_five_thousand.Length);
            Assert.AreEqual(2, fourty_five_thousand[0].Key);
            Assert.AreEqual(3, fourty_five_thousand[0].Value);
            Assert.AreEqual(3, fourty_five_thousand[1].Key);
            Assert.AreEqual(2, fourty_five_thousand[1].Value);
            Assert.AreEqual(5, fourty_five_thousand[2].Key);
            Assert.AreEqual(4, fourty_five_thousand[2].Value);
        }
    }
}