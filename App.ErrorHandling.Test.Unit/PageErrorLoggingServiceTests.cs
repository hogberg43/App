using System;
using System.Linq;
using System.Web;
using App.ErrorHandling.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.ErrorHandling.Test.Unit
{
    [TestClass]
    public class PageErrorLoggingServiceTests
    {
        private PageErrorLoggingService service;
        private FakePageErrorRepository repository;

        [TestInitialize]
        public void Setup()
        {
            repository = new FakePageErrorRepository();
            service = new PageErrorLoggingService(repository);
        }

        [TestCleanup]
        public void TearDown()
        {
            service = null;
            repository = null;
        }

        [TestMethod]
        public void GetErrorInfoFromException_GivenValidParametersReturnsErrorInfoObject()
        {
            var ex = new Exception("Test exception message", new Exception("Test Inner Exception"));
            var errorInfo = service.GetErrorInfoFromException(ex, "testpage.htm", "Tester", "TestApp");

            Assert.IsNotNull(errorInfo);
            Assert.IsTrue(errorInfo.ApplicationName == "TestApp");
            Assert.IsTrue(errorInfo.ErrorDate.Date == DateTime.Now.Date);
            Assert.IsTrue(errorInfo.ErrorDescription == ex.ToString());
            Assert.IsTrue(errorInfo.ErrorTitle == "Test exception message");
            Assert.IsTrue(errorInfo.OffendingUrl == "testpage.htm");
            Assert.IsTrue(errorInfo.UserId == "Tester");
        }

        [TestMethod]
        public void GetErrorInfoFromException_GivenHttpExceptionWithInnerExceptionReturnsInnertExceptionMessageAsTitle()
        {
            var ex = new HttpException("Test exception message", new Exception("Test Inner Exception"));
            var errorInfo = service.GetErrorInfoFromException(ex, "testpage.htm", "Tester", "TestApp");

            Assert.IsNotNull(errorInfo);
            Assert.IsTrue(errorInfo.ErrorTitle == "Test Inner Exception");
        }

        [TestMethod]
        public void GetErrorInfoFromException_GivenHttpExceptionWithNoInnerExceptionReturnsExceptionMessageAsTitle()
        {
            var ex = new HttpException("Test exception message");
            var errorInfo = service.GetErrorInfoFromException(ex, "testpage.htm", "Tester", "TestApp");

            Assert.IsNotNull(errorInfo);
            Assert.IsTrue(errorInfo.ErrorTitle == "Test exception message");
        }

        [TestMethod]
        public void AddLogEntry_GivenErrorInfoObjectAddsItToRepositoryAndReturnsId()
        {
            var errorInfo = new ErrorInfo();
            errorInfo.ErrorDescription = "Test Error Info added to the repository.";
            service.AddLogEntry(errorInfo);

            Assert.IsTrue(repository.GetAll().Any());
            Assert.IsTrue(errorInfo.LogId == 1);
        }
    }
}
