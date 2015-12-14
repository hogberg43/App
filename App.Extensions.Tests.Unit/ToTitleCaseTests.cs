using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class ToTitleCaseTests
    {
        private static void TestObject(string valToTest, string expectedResult)
        {
            var x = valToTest;
            var result = x.ToTitleCase();
            var expected = expectedResult;
            Assert.IsTrue(result == expected);
        }

        [TestMethod]
        public void GivenStringReturnsStringInTitleCase()
        {
            TestObject("this is a test", "This Is A Test");
        }

        [TestMethod]
        public void GivenNullReturnsEmptyString()
        {
            TestObject(null, "");
        }

        [TestMethod]
        public void GivenEmptyStringReturnsEmptyString()
        {
            TestObject("", "");
        }

        [TestMethod]
        public void GivenWhitespaceStringReturnsWhitespaceString()
        {
            TestObject("  ", "  ");
        }

        [TestMethod]
        public void GivenOneWordReturnsSameWordCapitalized()
        {
            TestObject("testing", "Testing");
        }
    }
}