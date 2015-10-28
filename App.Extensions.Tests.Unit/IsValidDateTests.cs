using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class IsValidDateTests
    {
        private static void TestObject(string valToTest, bool expectedResult)
        {
            string x = valToTest;
            var result = x.IsValidDate();
            var expected = expectedResult;
            Assert.IsTrue(result == expected);
        }

        [TestMethod]
        public void GivenNullReturnsFalse()
        {
            TestObject(null, false);
        }

        [TestMethod]
        public void GivenEmptyStringReturnsFalse()
        {
            TestObject("", false);
        }

        [TestMethod]
        public void GivenInvalidDateStringReturnsFalse()
        {
            TestObject("wewefljasdlkfj", false);
        }

        [TestMethod]
        public void GivenValidDateStringReturnsTrue()
        {
            TestObject("12/2/2012", true);
        }
    }
}