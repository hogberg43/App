using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class ToIntTests
    {
        private static void TestObject(object valToTest, int expectedResult)
        {
            object x = valToTest;
            var result = x.ToInt();
            var expected = expectedResult;
            Assert.IsTrue(result == expected);
        }

        [TestMethod]
        public void GivenStringWith22Returns22()
        {
            TestObject("22", 22);
        }
        [TestMethod]
        public void GivenFloatWith22Point2Returns22()
        {
            TestObject(22.2, 22);
        }

        [TestMethod]
        public void GivenNullReturnsMinusOne()
        {
            TestObject(null, -1);
        }

        [TestMethod]
        public void GivenMinusOneReturnsMinusOne()
        {
            TestObject(-1, -1);
        }

        [TestMethod]
        public void GivenZeroReturnsZero()
        {
            TestObject(0, 0);
        }

        [TestMethod]
        public void GivenStringMinusOneReturnsMinusOne()
        {
            TestObject("-1", -1);
        }

        [TestMethod]
        public void GivenStringZeroReturnsZero()
        {
            TestObject("0", 0);
        }

        [TestMethod]
        public void GivenListObjectReturnsMinusOne()
        {
            TestObject(new List<object>(), -1);
        }

        [TestMethod]
        public void GivenNaNStringReturnsMinusOne()
        {
            TestObject("NaN", -1);
        }

        [TestMethod]
        public void GivenEmptyStringReturnsMinusOne()
        {
            TestObject("", -1);
        }

        [TestMethod]
        public void GivenSpacesReturnsMinusOne()
        {
            TestObject("  ", -1);
        }
    }
}
