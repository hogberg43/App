using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Extensions.Tests.Unit
{
    [TestClass]
    public class StripDomainTests
    {
        [TestMethod]
        public void StripDomain_Given_domain_backlash_and_user_removes_everything_before_the_backslash()
        {
            // Arrange
            var domainUser = "somedomain\\someuser";

            // Act
            var actual = domainUser.StripDomain();

            // Assert
            Assert.AreEqual("someuser", actual);
        }

        [TestMethod]
        public void StripDomain_Given_there_is_no_backslash_return_the_original_string()
        {
            // Arrange
            var someUser = "someuser";

            // Act
            var actual = someUser.StripDomain();

            // Assert
            Assert.AreEqual(someUser, actual);
        }

        [TestMethod]
        public void StripDomain_Given_just_a_backslash_returns_()
        {
            // Arrange
            var text = @"\";

            // Act
            var actual = text.StripDomain();

            // Assert
            Assert.AreEqual("", actual);
        }

        [TestMethod]
        public void StripDomain_Given_and_empty_string_returns_an_empty_string()
        {
            // Arrange
            var text = "";

            // Act
            var actual = text.StripDomain();

            // Assert
            Assert.AreEqual("", actual);
        }
    }
}