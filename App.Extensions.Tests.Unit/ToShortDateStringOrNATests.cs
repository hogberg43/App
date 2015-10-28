using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class ToShortDateStringOrNATests
    {
        private static void TestObject(DateTime valToTest, string expectedResult)
        {
            var x = valToTest;
            var result = x.ToShortDateStringOrNA();
            var expected = expectedResult;
            Assert.IsTrue(result == expected);
        }

        [TestMethod]
        public void GivenDateGreaterThanMinDateReturnsDateString()
        {
            TestObject(new DateTime(2012, 12, 25), "12/25/2012");
        }

        [TestMethod]
        public void GivenNewDateReturnsNA()
        {
            TestObject(new DateTime(), "N/A");
        }

        [TestMethod]
        public void GivenMinDateReturnsNA()
        {
            TestObject(DateTime.MinValue, "N/A");
        }
    }
}