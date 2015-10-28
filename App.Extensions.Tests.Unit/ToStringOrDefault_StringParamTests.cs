using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class ToStringOrDefault_StringParamTests
    {
        private static void TestObject(string valToTest, string defaultValue, string expectedResult)
        {
            string x = valToTest;
            var result = x.ToStringOrDefault(defaultValue);
            var expected = expectedResult;
            Assert.IsTrue(result == expected);
        }

        [TestMethod]
        public void GivenNullReturnsDefaultString()
        {
            TestObject(null, "Default", "Default");
        }

        [TestMethod]
        public void GivenEmptyStringReturnsDefaultString()
        {
            TestObject("", "Default", "Default");
        }

        [TestMethod]
        public void GivenStringReturnsString()
        {
            TestObject("Given", "Default", "Given");
        }

        [TestMethod]
        public void GivenWhiteSpaceReturnsString()
        {
            TestObject(" ", "Default", "Default");
        }
    }
}