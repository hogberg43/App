using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class ToInt_WithDefaultTests
    {
        private static void TestObject(object valToTest, int defaultValue, int expectedResult)
        {
            object x = valToTest;
            var result = x.ToInt(defaultValue);
            var expected = expectedResult;
            Assert.IsTrue(result == expected);
        }

        [TestMethod]
        public void GivenNullReturns5()
        {
            TestObject(null, 5, 5);
        }

        [TestMethod]
        public void GivenEmptyStringReturns5AsDefault()
        {
            TestObject("", 5, 5);
        }

        [TestMethod]
        public void GivenMiscObjectReturns5AsDefault()
        {
            TestObject(new {TestProp = "1"}, 5, 5);
        }

        [TestMethod]
        public void Given5Returns5EvenWhenDefaultIs1()
        {
            TestObject(5, 1, 5);
        }

        [TestMethod]
        public void Given23Point23Returns23()
        {
            TestObject(23.23, 1, 23);
        }

        [TestMethod]
        public void GivenMinusOneReturnsMinusOneEvenWhenDefaultIs5()
        {
            TestObject(-1, 5, -1);
        }
    }
}