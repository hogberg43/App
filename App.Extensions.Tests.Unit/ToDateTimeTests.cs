using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class ToDateTimeTests
    {
        private static void TestObject(object valToTest, DateTime expectedResult)
        {
            object x = valToTest;
            var result = x.ToDateTime();
            var expected = expectedResult;
            Assert.IsTrue(result == expected);
        }

        [TestMethod]
        public void GivenNullReturnsMinValue()
        {
            TestObject(null, DateTime.MinValue);
        }

        [TestMethod]
        public void GivenMiscObjectReturnsMinValue()
        {
            TestObject(new { TestProp = "Test"}, DateTime.MinValue);
        }

        [TestMethod]
        public void GivenStringDateReturnsDateTime()
        {
            TestObject("12/1/2012", new DateTime(2012, 12, 1));
        }

        [TestMethod]
        public void GivenInvalidStringReturnsDateTime()
        {
            TestObject("alsjdfkjasd", DateTime.MinValue);
        }

        [TestMethod]
        public void GivenIntReturnsDateTime()
        {
            TestObject(12, DateTime.MinValue);
        }
    }
}