using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class ToStringOrDefault_ObjectParamTests
    {
        private static void TestObject(object valToTest, string defaultValue, string expectedResult)
        {
            object x = valToTest;
            var result = x.ToStringOrDefault(defaultValue);
            var expected = expectedResult;
            Assert.IsTrue(result == expected, string.Format("Result: {0}, Expected: {1}", result, expected));
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
        public void GivenWhiteSpaceReturnsString()
        {
            TestObject(" ", "Default", "Default");
        }
        [TestMethod]
        public void GivenStringReturnsString()
        {
            TestObject("Given", "Default", "Given");
        }

        [TestMethod]
        public void Given1ReturnsOneAsString()
        {
            TestObject(1, "Default", "1");
        }

        [TestMethod]
        public void GivenMiscObjectReturnsObjectToString()
        {
            TestObject(new { TestProp = "TestVal" }, "Default", "{ TestProp = TestVal }");
        }
    }
}