using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class ParseForXmlTests
    {
        private static void TestValue(string valToTest, string expectedResult)
        {
            var result = valToTest.ParseForXml();
            var expected = expectedResult;

            Assert.IsTrue(result == expected);
        }

        [TestMethod]
        public void GivenEmptyStringReturnsEmptyString()
        {
            TestValue("", "");
        }
        
        [TestMethod]
        public void GivenAmpersandReturnsHTMLCode()
        {
            TestValue("&", "&amp;");
        }
        [TestMethod]
        public void GivenLeftWedgeReturnsHTMLCode()
        {
            TestValue("<", "&lt;");
        }
        [TestMethod]
        public void GivenRightWedgeReturnsHTMLCode()
        {
            TestValue(">", "&gt;");
        }
        [TestMethod]
        public void GivenSingleQuoteReturnsHTMLCode()
        {
            TestValue("'", "&#39;");
        }
        [TestMethod]
        public void GivenDoubleQuoteReturnsHTMLCode()
        {
            TestValue("\"", "&quot;");
        }
        [TestMethod]
        public void GivenStringWithNoCharactersToConvertReturnsOriginalString()
        {
            TestValue("Hi. My name is Mister Tester.", "Hi. My name is Mister Tester.");
        }
        [TestMethod]
        public void GivenStringWithCharactersToConvertReturnsParsedString()
        {
            TestValue("Hi. This is Mister Tester's \"HTML\" <code/> & some other stuff.", 
                "Hi. This is Mister Tester&#39;s &quot;HTML&quot; &lt;code/&gt; &amp; some other stuff.");
        }
    }
}
