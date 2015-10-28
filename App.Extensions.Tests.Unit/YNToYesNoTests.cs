using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class YNToYesNoTests
    {
        private static void TestObject(string valToTest, string expectedResult)
        {
            string x = valToTest;
            var result = x.YNToYesNo();
            var expected = expectedResult;
            Assert.IsTrue(result == expected);
        }

        [TestMethod]
        public void GivenLowercaseYReturnsYes()
        {
            TestObject("y", "Yes");
        }
        [TestMethod]
        public void GivenUppercaseYReturnsYes()
        {
            TestObject("Y", "Yes");
        }
        [TestMethod]
        public void GivenLowercaseNReturnsNo()
        {
            TestObject("n", "No");
        }
        [TestMethod]
        public void GivenUppercaseNReturnsNo()
        {
            TestObject("N", "No");
        }
        [TestMethod]
        public void GivenOtherTextReturnsOtherTextUnchanged()
        {
            TestObject("Some Text", "Some Text");
        }
        [TestMethod]
        public void GivenNullReturnsEmptyString()
        {
            TestObject(null, "");
        }
    }
}