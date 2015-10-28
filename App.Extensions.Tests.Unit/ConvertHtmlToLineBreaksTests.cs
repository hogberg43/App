using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class ConvertHtmlToLineBreaksTests
    {
        private static void TestObject(string valToTest, string expectedResult)
        {
            string x = valToTest;
            var result = x.ConvertHtmlToLineBreaks();
            var expected = expectedResult;
            Assert.IsTrue(result == expected);
        }

        [TestMethod]
        public void GivenStringWithNoHTMLBreaksReturnsString()
        {
            TestObject("No Breaks.", "No Breaks.");
        }

        [TestMethod]
        public void GivenAllSixBRCasesReturnsSixReturnNexts()
        {
            TestObject("<br/><BR/><br /><BR /><br><BR>", "\r\n\r\n\r\n\r\n\r\n\r\n");
        }
    }
}