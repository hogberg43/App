using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class ToIntNullableTests
    {
        private static void TestObject(object valToTest, int? expectedResult)
        {
            object x = valToTest;
            var expected = expectedResult;
            var result = x.ToIntNullable();

            Assert.IsTrue(result == expected);
        }

        [TestMethod]
        public void GivenNullReturnsNull()
        {
            TestObject(null, null);
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
        public void GivenListObjectReturnsNull()
        {
            TestObject(new List<object>(), null);
        }

        [TestMethod]
        public void GivenNaNStringReturnsNull()
        {
            TestObject("NaN", null);
        }

        [TestMethod]
        public void GivenEmptyStringReturnsNull()
        {
            TestObject("", null);
        }

        [TestMethod]
        public void GivenSpacesReturnsMinusOne()
        {
            TestObject("  ", null);
        }
    }
}