using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyLibs.Tests
{
    [TestClass]
    public class RefTests
    {
        [TestMethod]
        public void RefTest()
        {
            var i = 0;
            var iRef = new Ref<int>(() => i, (value) => i = value);

            Assert.AreEqual(0, i);
            Assert.AreEqual(0, iRef.Value);
            Assert.AreEqual(i, iRef.Value);

            i = 1;
            Assert.AreEqual(1, i);
            Assert.AreEqual(1, iRef.Value);
            Assert.AreEqual(i, iRef.Value);

            iRef.Value = 2;
            Assert.AreEqual(2, i);
            Assert.AreEqual(2, iRef.Value);
            Assert.AreEqual(i, iRef.Value);
        }
    }
}