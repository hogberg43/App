using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class IsValidDate_DateFormatTests
    {
        private static void TestObject(string valToTest, string dateFormat, bool expectedResult)
        {
            string x = valToTest;
            var result = x.IsValidDate(dateFormat);
            var expected = expectedResult;
            Assert.IsTrue(result == expected);
        }

        [TestMethod]
        public void GivenNullReturnsFalse()
        {
            TestObject(null, "", false);
        }

        [TestMethod]
        public void GivenEmptyStringReturnsFalse()
        {
            TestObject("", "", false);
        }

        [TestMethod]
        public void GivenInvalidDateStringReturnsFalse()
        {
            TestObject("wewefljasdlkfj", "", false);
        }

        [TestMethod]
        public void GivenValidDateStringWithNoFormatReturnsTrue()
        {
            TestObject("12/2/2012", "", true);
        }

        [TestMethod]
        public void GivenValidDateStringWithMonthDayYearFormatReturnsTrue()
        {
            TestObject("12/02/2012", "MM/dd/yyyy", true);
        }

        [TestMethod]
        public void GivenInValidDateStringWithMonthDayYearHourMinuteFormatReturnsFalse()
        {
            TestObject("12/5/2014", "MM/d/yyyy hh:mm", false);
        }
        [TestMethod]
        public void GivenValidDateStringWithMonthDayYearHourMinuteFormatReturnsTrue()
        {
            TestObject("12/5/2014 08:00", "MM/d/yyyy hh:mm", true);
        }
    }
}