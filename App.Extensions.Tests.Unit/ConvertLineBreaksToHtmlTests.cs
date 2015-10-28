using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class ConvertLineBreaksToHtmlTests
    {
        private static void TestObject(string valToTest, string expectedResult)
        {
            string x = valToTest;
            var result = x.ConvertLineBreaksToHtml();
            var expected = expectedResult;
            Assert.IsTrue(result == expected);
        }

        [TestMethod]
        public void GivenStringWithNoLineBreaksReturnsString()
        {
            TestObject("This is a single line.", "This is a single line.");
        }

        [TestMethod]
        public void GivenStringWithLineBreaksReturnsStringWithHtmlBreak()
        {
            TestObject("This is line one.\r\nThis is line two.", "This is line one.<br />This is line two.");
        }
    }
}