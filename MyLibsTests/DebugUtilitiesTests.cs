using MyLibs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyLibs.Tests
{
    [TestClass]
    public class DebugUtilitiesTests
    {
        [TestMethod]
        public void GetCallerFilePathTest()
        {
            Assert.IsTrue(DebugUtilities.GetCallerFilePath().EndsWith(@"\MyLibs\MyLibsTests\DebugUtilitiesTests.cs"));
        }

        [TestMethod]
        public void GetCallerFileTest()
        {
            Assert.AreEqual("DebugUtilitiesTests.cs", DebugUtilities.GetCallerFile());
        }

        [TestMethod]
        public void GetCallerLineNumberTest()
        {
            Assert.AreEqual(24, DebugUtilities.GetCallerLineNumber());
        }

        [TestMethod]
        public void GetCallerMethodNameTest()
        {
            Assert.AreEqual("GetCallerMethodNameTest", DebugUtilities.GetCallerMethodName());
        }

        [TestMethod]
        public void GetCallerExpressionTest()
        {
            var expression = DebugUtilities.GetExpression(1 + 2);
            Assert.AreEqual("", expression.expression);
            Assert.AreEqual(3, expression.result);

            // When 'GetCallerExpression' has been implemented, result should look like this.
            // Check progress here: https://github.com/dotnet/csharplang/issues/287
            //var expression = DebugUtilities.GetExpression(1 + 2);
            //Assert.AreEqual("1 + 2", expression.expression);
            //Assert.AreEqual(3, expression.result);

            //var expression2 = DebugUtilities.GetExpression(() => TestExpression(1, 2));
            //Assert.AreEqual("TestExpression(1, 2)", expression2);
        }

        private static void TestExpression(int a, int b) { }
    }
}