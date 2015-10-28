using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class ToShortDateStringOrNA_DefaultValueTests
    {
        private static void TestObject(DateTime valToTest, string defaultValue, string expectedResult)
        {
            var x = valToTest;
            var result = x.ToShortDateStringOrNA(defaultValue);
            var expected = expectedResult;
            Assert.IsTrue(result == expected);
        }

        [TestMethod]
        public void GivenDateGreaterThanMinDateReturnsDateString()
        {
            TestObject(new DateTime(2012, 12, 25), "No Date", "12/25/2012");
        }

        [TestMethod]
        public void GivenNewDateReturnsNA()
        {
            TestObject(new DateTime(), "No Date", "No Date");
        }

        [TestMethod]
        public void GivenMinDateReturnsNA()
        {
            TestObject(DateTime.MinValue, "No Date", "No Date");
        }
    }
}