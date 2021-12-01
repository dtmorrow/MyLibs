using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MyLibs.Tests
{
    [TestClass]
    public class ResultTests
    {
        private static Result<int> Divide(int a, int b)
        {
            if (b == 0)
            {
                return new Result<int>(false);
            }

            return new Result<int>(true, a / b);
        }

        private enum Compare { Equal, LessThan, GreaterThan };
        private static Result<int, Compare> AreEqual(int a, int b)
        {
            if (a == b)
            {
                return new Result<int, Compare>(true, Compare.Equal, b);
            }
            else if (a < b)
            {
                return new Result<int, Compare>(false, Compare.LessThan, b);
            }
            else
            {
                return new Result<int, Compare>(false, Compare.GreaterThan, b);
            }
        }

        [TestMethod]
        public void ResultTest()
        {
            // Test Boolean Result
            var success = Divide(10, 5);
            Assert.IsTrue(success.Success);
            Assert.AreEqual(2, success.Value);

            var fail = Divide(10, 0);
            Assert.IsFalse(fail.Success);

            // Test Exception Result
            var noException = Result.GetResult(() => 10 / 5);
            Assert.IsTrue(noException.Success);
            Assert.IsNull(noException.Error);
            Assert.AreEqual(2, noException.Value);

            var zero = 0;
            var exception = Result.GetResult(() => 10 / zero);
            Assert.IsFalse(exception.Success);
            Assert.AreEqual(typeof(DivideByZeroException), exception.Error.GetType());

            // Test Enum Result
            var equal = AreEqual(5, 5);
            Assert.IsTrue(equal.Success);
            Assert.AreEqual(Compare.Equal, equal.Error);

            var lessThan = AreEqual(0, 5);
            Assert.IsFalse(lessThan.Success);
            Assert.AreEqual(Compare.LessThan, lessThan.Error);

            var greaterThan = AreEqual(10, 5);
            Assert.IsFalse(greaterThan.Success);
            Assert.AreEqual(Compare.GreaterThan, greaterThan.Error);
        }
    }
}
