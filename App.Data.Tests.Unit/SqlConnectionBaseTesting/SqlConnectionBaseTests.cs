using System.Data.SqlClient;
using App.Data.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Data.Tests.Unit.SqlConnectionBaseTesting
{
    [TestClass]
    public class SqlConnectionBaseTests
    {
        [TestMethod]
        public void GetConnection_GivenValidConfigKeyReturnsSqlConnection()
        {
            // Arrange
            const string configKey = "ConnectionTesting";

            // Act
            var actual = ConcreteSqlConnections.ExecuteGetConnectionMethod(configKey);

            // Assert
            Assert.AreEqual("", actual.ConnectionString); //TODO: add a connection string matching app.config
        }

        [TestMethod]
        public void GetConnection_GivenValidAppSettingKeyNotInConnectionStringsReturnsSqlConnection()
        {
            // Arrange
            const string configKey = "AppConnectionTesting";

            // Act
            var actual = ConcreteSqlConnections.ExecuteGetConnectionMethod(configKey);

            // Assert
            Assert.AreEqual("", actual.ConnectionString); //TODO: add a connection string matching app.config
        }

        [TestMethod]
        [ExpectedException(typeof(SqlConnectionException))]
        public void GetConnection_GivenInvalidConfigKeyThrowsSqlConnectionException()
        {
            // Arrange
            const string configKey = "asdfsd";

            // Act
            ConcreteSqlConnections.ExecuteGetConnectionMethod(configKey);

            // Assert
            Assert.AreEqual(0, 0);
        }
    }

    class ConcreteSqlConnections : SqlConnectionsBase
    {
        public static SqlConnection ExecuteGetConnectionMethod(string key)
        {
            return GetConnection(key);
        }
    }
}
