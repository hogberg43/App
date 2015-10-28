using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using App.Data.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Data.Tests.Unit.SqlDbTesting
{
    /// <summary>
    /// Data Access Layer Tests
    /// </summary>
    /// <remarks>
    /// Each Test method includes the SqlDb method being tested as the prefix in the name.
    /// </remarks>
    [TestClass]
    public class SqlDbTests
    {
        private SqlConnection _testConn;

        /// <summary>
        /// Connection to reuse where possible when running the tests.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            var connString = ConfigurationManager.ConnectionStrings["ConnectionTesting"].ToString();
            _testConn = new SqlConnection(connString);
        }

        #region OpenConnection Tests
        [TestMethod]
        public void OpenConnection_OpensSqlServerConnection()
        {
            // Arrange
            var database = new SqlDb(_testConn);

            // Act
            database.OpenConnection();

            // Assert
            Assert.IsTrue(_testConn.State == ConnectionState.Open);
        }

        [TestMethod]
        [ExpectedException(typeof(DBException))]
        public void OpenConnection_ThrowsDBExceptionWhenConnectionIsEmpty()
        {
            // Arrange
            var database = new SqlDb(new SqlConnection());

            // Act
            database.OpenConnection();

            // Assert
            Assert.IsTrue(_testConn.State == ConnectionState.Open);
        }

        [TestMethod]
        public void OpenConnection_OpensSqlServerConnectionWhenCurrentConnectionHasBeenDisposedPrior()
        {
            var database = new SqlDb(_testConn);
            _testConn.Dispose();

            database.OpenConnection();

            Assert.IsTrue(_testConn.State == ConnectionState.Open);
        }

        #endregion

        #region CloseConnection Tests
        [TestMethod]
        public void CloseConnection_ClosesAnOpenSqlServerConnection()
        {
            // Arrange
            _testConn.Open();
            var database = new SqlDb(_testConn);

            // Act
            database.CloseConnection();

            // Assert
            Assert.IsTrue(_testConn.State == ConnectionState.Closed);
        }
        [TestMethod]
        public void CloseConnection_ClosesSqlServerConnectionThatIsAlreadyClosed()
        {
            // Arrange
            if (_testConn.State == ConnectionState.Closed) _testConn.Close();
            var database = new SqlDb(_testConn);

            // Act
            database.CloseConnection();

            // Assert
            Assert.IsTrue(_testConn.State == ConnectionState.Closed);
        }
        [TestMethod]
        public void CloseConnection_ClosesEmptySqlServerConnection()
        {
            // Arrange
            var database = new SqlDb(new SqlConnection());

            // Act
            database.CloseConnection();

            // Assert
            Assert.IsTrue(_testConn.State == ConnectionState.Closed);
        }
        #endregion

        #region Execute Tests
        [TestMethod]
        public void Execute_SucceedsWhenGivenAValidCommand()
        {
            var database = new SqlDb(_testConn);
            var testCommand = StoredProcedureFactory.Create("SqlDb_ExecuteTestProcedure1");
            
            database.Execute(testCommand, "Execute Test Failed");

            Assert.AreEqual(0,0);
        }
        [TestMethod]
        public void Execute_SucceedsWhenGivenAValidCommandAndNullStringParameter()
        {
            var database = new SqlDb(_testConn);
            var testCommand = StoredProcedureFactory.Create("SqlDb_ExecuteTestProcedure2", 
                new SqlParameter("@p1", null));

            database.Execute(testCommand, "Execute Test Failed");

            Assert.AreEqual(0, 0);
        }
        [TestMethod]
        [ExpectedException(typeof(DBCommandException))]
        public void Execute_ThrowsADBExceptionWhenGivenACommandThatIsNotFound()
        {
            var database = new SqlDb(_testConn);
            var testCommand = StoredProcedureFactory.Create("asdfsd");
            
            database.Execute(testCommand, "Execute Test Failed");

            Assert.AreEqual(0, 0);
        }
        #endregion

        #region ExecuteTransaction Tests
        [TestMethod]
        public void ExecuteTransaction_ReturnsTransactionStatusGivenAValidDataTable()
        {
            var database = new SqlDb(_testConn);
            var testCommand = StoredProcedureFactory.Create("SqlDb_ExecuteTransactionAndReturnTransactionStatusTestProcedure1");

            var status = database.ExecuteTransaction(testCommand, "Execute Transaction Failed");

            Assert.AreEqual(string.Format("{0}, {1}, {2}", TransactionStatusCode.Cancelled, "There were no source records. Operation not executed", ""), status.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(DBCommandException))]
        public void ExecuteTransaction_ThrowsDbCommandExceptionWhenCommandDoesntExist()
        {
            var database = new SqlDb(_testConn);
            var testCommand = StoredProcedureFactory.Create("BogusCommand");
            database.ExecuteTransaction(testCommand, "Execute Transaction Failed");
        }

        [TestMethod]
        [ExpectedException(typeof(ExecuteTransactionFormatException))]
        public void ExecuteTransaction_ThrowsExceptionGivenADataTableWithInvalidColumns()
        {
            var database = new SqlDb(_testConn);
            var testCommand = StoredProcedureFactory.Create("SqlDb_ExecuteTransactionAndReturnTransactionStatusTestProcedure2");
            try
            {
                database.ExecuteTransaction(testCommand, "Execute Transaction Failed");
            }
            catch (ExecuteTransactionFormatException testException)
            {
                TestUtility.TestExceptionMessage(testException, 
                    "The data table returned cannot be used by the TransactionStatus object since it does not contain one or all of the following columns: ErrorCode, ErrorMessage, TransactionInfo.");
            }
            catch (Exception ex)
            {
                throw new Exception("Test Failed", ex);
            }

        }
        
        [TestMethod]
        [ExpectedException(typeof(ExecuteTransactionFormatException))]
        public void ExecuteTransaction_ThrowsExceptionGivenADataTableWithInvalidRows()
        {
            var database = new SqlDb(_testConn);
            var testCommand = StoredProcedureFactory.Create("SqlDb_ExecuteTransactionAndReturnTransactionStatusTestProcedure3");

            try
            {
                database.ExecuteTransaction(testCommand, "Execute Transaction Failed");
            }
            catch (ExecuteTransactionFormatException testException)
            {
                TestUtility.TestExceptionMessage(testException, 
                    "The TransactionStatus object expects exactly one row to be returned by the procedure.");
            }
            catch (Exception ex)
            {
                throw new Exception("Test Failed", ex);
            }
        }
        
        #endregion

        #region ExecuteAndReturnScalar
        [TestMethod]
        public void ExecuteAndReturnScalar_InsertsItemAndReturnsIdOfAddedField()
        {
            const int expectedValue = 1;
            var database = new SqlDb(_testConn);
            var testProcedure = StoredProcedureFactory.Create("SqlDb_ExecuteAndReturnScalarTestProcedure1", 
                new SqlParameter("@Param1", "Testing123"));

            object actualObject = database.ExecuteAndReturnScalar(testProcedure, "ExecuteAndReturnScalar test failed.");
            
            var actualValue = Convert.ToInt32(actualObject);
            Assert.AreEqual(expectedValue, actualValue);
        }
        [TestMethod]
        [ExpectedException(typeof(DBCommandException))]
        public void ExecuteAndReturnScalar_ThrowsADBCommandExceptionWhenProcedureFails()
        {
            var database = new SqlDb(_testConn);
            // don't pass params to cause a failure
            var testProcedure = StoredProcedureFactory.Create("SqlDb_ExecuteAndReturnScalarTestProcedure1");

            database.ExecuteAndReturnScalar(testProcedure, "ExecuteAndReturnScalar test failed.");

            Assert.AreEqual(0, 0);
        }
        #endregion

        #region ExecuteAndReturnDataTable
        [TestMethod]
        public void ExecuteAndReturnDataTable_GivenAValidCommandReturnsExpectedDataTable()
        {
            var database = new SqlDb(_testConn);
            var testCommand = StoredProcedureFactory.Create("SqlDb_ExecuteAndReturnDataTableTestProcedure1");

            var testData = new List<TestTableItem>
                               {
                                   new TestTableItem {ID = 1, Field1 = "Testing123"},
                                   new TestTableItem {ID = 2, Field1 = "TestRecord"}
                               };
            var dtExpectedResult = TestData.ListToTable(testData);
            
            var dtActualResult = database.ExecuteAndReturnDataTable(testCommand, "ExecuteAndReturnDataTable test failed");

            DataTableAssert.AllDataMatches(dtExpectedResult, dtActualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(DBCommandException))]
        public void ExecuteAndReturnDataTable_ThrowsAnExceptionGivenAnInvalidCommand()
        {
            var database = new SqlDb(_testConn);
            var testCommand = new SqlCommand();
            
            database.ExecuteAndReturnDataTable(testCommand, "ExecuteAndReturnDataTable test failed");

            Assert.AreEqual(0,0);
        }

	    #endregion

        #region ExecuteAndReturnDataSet
        [TestMethod]
        public void ExecuteAndReturnDataSet_GivenAValidCommandReturnsExpectedDataSet()
        {
            var database = new SqlDb(_testConn);
            var testCommand = StoredProcedureFactory.Create("SqlDb_ExecuteAndReturnDataSetTestProcedure1");

            var testData1 = new List<TestTableItem>();
            var testData2 = new List<TestTableItem>();

            var testDataTable1 = TestData.ListToTable(testData1);
            var testDataTable2 = TestData.ListToTable(testData2);

            var expectedDataSet = new DataSet();
            expectedDataSet.Tables.Add(testDataTable1);
            expectedDataSet.Tables.Add(testDataTable2);

            var actualDataSet = database.ExecuteAndReturnDataSet(testCommand, "ExecuteAndReturnDataSet test failed");

            DataSetAssert.AllDataMatches(expectedDataSet, actualDataSet);

        }

        [TestMethod]
        [ExpectedException(typeof(DBCommandException))]
        public void ExecuteAndReturnDataSet_ThrowsAnExceptionWhenCommandDoesntExist()
        {
            var database = new SqlDb(_testConn);
            var testCommand = StoredProcedureFactory.Create("BogusCommand");
            database.ExecuteAndReturnDataSet(testCommand, "ExecuteAndReturnDataSet test failed");
        }

        [TestMethod]
        [ExpectedException(typeof(DBCommandException))]
        public void ExecuteAndReturnDataSet_ThrowsAnExceptionGivenAnInvalidCommand()
        {
            var database = new SqlDb(_testConn);
            var testCommand = new SqlCommand();

            database.ExecuteAndReturnDataTable(testCommand, "ExecuteAndReturnDataTable test failed");

            Assert.AreEqual(0, 0);
        }
        #endregion

        /// <summary>
        /// Cleanup test connection
        /// </summary>
        [TestCleanup]
        public void TearDown()
        {
            if (_testConn.State == ConnectionState.Open) _testConn.Close();
            _testConn.Dispose();
        }
    }
}
