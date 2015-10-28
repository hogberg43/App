using System;
using System.Data.SqlClient;
using App.Data.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Data.Tests.Unit.StoredProcedureFactoryTesting
{
    [TestClass]
    public class StoredProcedureFactoryTests
    {

        [TestInitialize]
        public void Setup()
        {

        }

        [TestCleanup]
        public void TearDown()
        {

        }

        [TestMethod]
        public void Create_GivenAllParametersCreatesCommand()
        {
            var com = StoredProcedureFactory.Create("SomeProcedure", new SqlParameter("@SomeParameter", "SomeValue"));
            Assert.IsNotNull(com);
        }

        [TestMethod]
        public void Create_GivenAllParametersWithNullValueStillCreatesCommand()
        {
            var com = StoredProcedureFactory.Create("SomeProcedure", new SqlParameter("@SomeParameter", null));
            Assert.IsNotNull(com);
            Assert.IsTrue(com.Parameters[0].Value.ToString() == "");
        }

        [TestMethod]
        public void Create_GivenAllParametersWithDateTimeValueStillCreatesCommand()
        {
            var com = StoredProcedureFactory.Create("SomeProcedure", new SqlParameter("@SomeParameter", DateTime.Now));
            Assert.IsNotNull(com);
            Assert.IsTrue(com.Parameters[0].Value.ToString().Contains(DateTime.Now.ToShortDateString()));
        }
    }
}
