using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLibs.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MyLibs.Collections.Tests
{
    [TestClass]
    public class ItemBagTests
    {
        [TestMethod]
        public void GetEnumeratorTest()
        {
            var list = new List<int>();
            var bag = new ItemBag<int>();

            Assert.AreEqual(list.GetEnumerator().GetType(), bag.GetEnumerator().GetType());
        }

        [TestMethod]
        public void AddTest()
        {
            var sb = new StringBuilder();
            var bag = new ItemBag<int>();

            bag.ProcessItems((i) => sb.Append(i));
            Assert.AreEqual("", sb.ToString());
            sb.Clear();

            bag.Add(1);

            bag.ProcessItems((i) => sb.Append(i));
            Assert.AreEqual("1", sb.ToString());
            sb.Clear();

            bag.Add(2);

            bag.ProcessItems((i) => sb.Append(i));
            Assert.AreEqual("12", sb.ToString());
            sb.Clear();

            bag.Add(3);

            bag.ProcessItems((i) => sb.Append(i));
            Assert.AreEqual("123", sb.ToString());
            sb.Clear();
        }

        [TestMethod]
        public void AddRangeTest()
        {
            var sb = new StringBuilder();
            var bag = new ItemBag<int>();

            bag.ProcessItems((i) => sb.Append(i));
            Assert.AreEqual("", sb.ToString());
            sb.Clear();

            bag.AddRange(Enumerable.Range(1, 9));

            bag.ProcessItems((i) => sb.Append(i));
            Assert.AreEqual("123456789", sb.ToString());
        }

        [TestMethod]
        public void ClearTest()
        {
            var sb = new StringBuilder();
            var bag = new ItemBag<int>(Enumerable.Range(1, 9));

            bag.ProcessItems((i) => sb.Append(i));
            Assert.AreEqual("123456789", sb.ToString());

            sb.Clear();
            bag.Clear();

            bag.ProcessItems((i) => sb.Append(i));
            Assert.AreEqual("", sb.ToString());
        }

        [TestMethod]
        public void RemoveTest()
        {
            var sb = new StringBuilder();
            var bag = new ItemBag<int>(Enumerable.Range(1, 9));

            bag.ProcessItems((i) => sb.Append(i));
            Assert.AreEqual("123456789", SortString(sb.ToString()));

            bag.Remove(1);

            sb.Clear();
            bag.ProcessItems((i) => sb.Append(i));
            Assert.AreEqual("23456789", SortString(sb.ToString()));

            bag.Remove(2);

            sb.Clear();
            bag.ProcessItems((i) => sb.Append(i));
            Assert.AreEqual("3456789", SortString(sb.ToString()));

            bag.Remove(3);

            sb.Clear();
            bag.ProcessItems((i) => sb.Append(i));
            Assert.AreEqual("456789", SortString(sb.ToString()));
        }

        [TestMethod]
        public void RemoveAllItemsTest()
        {
            var sb = new StringBuilder();
            var bag = new ItemBag<int>(Enumerable.Range(1, 9));

            bag.RemoveAll(new int[] { 1, 2, 3, 7, 8, 9 });

            bag.ProcessItems((i) => sb.Append(i));

            Assert.AreEqual("456", SortString(sb.ToString()));
        }

        [TestMethod]
        public void RemoveAllPredicateTest()
        {
            var sb = new StringBuilder();
            var bag = new ItemBag<int>(Enumerable.Range(1, 9));

            bag.RemoveAll((i) => i % 2 == 0);

            bag.ProcessItems((i) => sb.Append(i));

            Assert.AreEqual("13579", SortString(sb.ToString()));
        }

        [TestMethod]
        public void ProcessItemsTest()
        {
            var sb = new StringBuilder();
            var bag = new ItemBag<int>(Enumerable.Range(1, 9));

            bag.ProcessItems((i) => sb.Append(i));

            Assert.AreEqual("123456789", sb.ToString());
        }

        [TestMethod]
        public void ProcessItemsAndRemoveTest()
        {
            var sb = new StringBuilder();
            var bag = new ItemBag<int>(Enumerable.Range(1, 9));

            bag.ProcessItems((i) => sb.Append(i), (i) => i % 2 == 0);

            Assert.AreEqual("123456789", SortString(sb.ToString()));

            sb.Clear();
            bag.ProcessItems((i) => sb.Append(i));

            Assert.AreEqual("13579", SortString(sb.ToString()));
        }

        private static string SortString(string value)
        {
            return new string(value.OrderBy(c => c).ToArray());
        }
    }
}