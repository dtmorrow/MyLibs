using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLibs.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibs.Collections.Tests
{
    [TestClass]
    public class CollectionUtilitiesTests
    {
        [TestMethod]
        public void GetCircularIndexTest()
        {
            Assert.AreEqual(0, CollectionUtilities.GetCircularIndex(0, 3));
            Assert.AreEqual(1, CollectionUtilities.GetCircularIndex(1, 3));
            Assert.AreEqual(2, CollectionUtilities.GetCircularIndex(2, 3));
            Assert.AreEqual(0, CollectionUtilities.GetCircularIndex(3, 3));
            Assert.AreEqual(2, CollectionUtilities.GetCircularIndex(-1, 3));
            Assert.AreEqual(1, CollectionUtilities.GetCircularIndex(-2, 3));
            Assert.AreEqual(0, CollectionUtilities.GetCircularIndex(-3, 3));

            Assert.AreEqual(1, CollectionUtilities.GetCircularIndex((long)int.MaxValue + 1, int.MaxValue));
        }
    }
}