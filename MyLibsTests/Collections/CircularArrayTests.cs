using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLibs.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MyLibs.Collections.Tests
{
    [TestClass]
    public class CircularArrayTests
    {
        [TestMethod]
        public void CircularArrayTest()
        {
            int[] expected = new int[] { 1, 2, 3, 4, 5 };

            var array = new CircularArray<int>(expected.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                array[i] = expected[i];
            }

            CollectionAssert.AreEqual(expected, array.Array);

            array[-1] = 10;
            array[-2] = 9;
            array[-3] = 8;
            array[-4] = 7;
            array[-5] = 6;

            CollectionAssert.AreEqual(expected.Select(value => value + 5).ToArray(), array.Array);

            array[(long)-6] = 15;
            array[(long)-7] = 14;
            array[(long)-8] = 13;
            array[(long)-9] = 12;
            array[(long)-10] = 11;

            CollectionAssert.AreEqual(expected.Select(value => value + 10).ToArray(), array.Array);

            array[5] = 16;
            array[6] = 17;
            array[7] = 18;
            array[8] = 19;
            array[9] = 20;

            CollectionAssert.AreEqual(expected.Select(value => value + 15).ToArray(), array.Array);

            array[(long)10] = 21;
            array[(long)11] = 22;
            array[(long)12] = 23;
            array[(long)13] = 24;
            array[(long)14] = 25;

            CollectionAssert.AreEqual(expected.Select(value => value + 20).ToArray(), array.Array);

            Assert.AreEqual(expected.Length, array.Array.Length);
            Assert.AreEqual(expected.IsSynchronized, array.Array.IsSynchronized);
            Assert.AreEqual(expected.SyncRoot.GetType(), array.Array.SyncRoot.GetType());
            Assert.AreEqual(expected.IsReadOnly, array.Array.IsReadOnly);

            array.Array.CopyTo(expected, 0);
            CollectionAssert.AreEqual(array.Array, expected);

            CollectionAssert.Contains(array.Array, 21);
            CollectionAssert.DoesNotContain(array.Array, 100);
            Assert.AreEqual(1, Array.IndexOf(array.Array, 22));
            Assert.AreEqual(-1, Array.IndexOf(array.Array, 100));
        }
    }
}