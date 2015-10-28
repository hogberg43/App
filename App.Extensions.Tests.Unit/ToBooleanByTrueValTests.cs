using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class ToBooleanByTrueValTests
    {
        private static void TestObject(string valToTest, string trueValue, bool expectedResult)
        {
            var x = valToTest;
            var result = x.ToBooleanByTrueVal(trueValue);
            var expected = expectedResult;
            Assert.IsTrue(result == expected);
        }

        [TestMethod]
        public void GivenNullReturnsFalse()
        {
            TestObject(null, "True", false);
        }

        [TestMethod]
        public void GivenEmptyStringReturnsFalse()
        {
            TestObject("", "True", false);
        }

        [TestMethod]
        public void GivenTrueValueAsStringReturnsTrue()
        {
            TestObject("True", "True", true);
        }
    }
}