using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Data.Tests.Unit.TransactionStatusTesting
{
    [TestClass]
    public class TransactionStatusTests
    {
        [TestMethod]
        public void Constructor_Empty()
        {
            var actual = new TransactionStatus();
            
            Assert.AreEqual(TransactionStatusCode.NotExecuted, actual.StatusCode);
            Assert.AreEqual("", actual.ErrorMessage);
            Assert.AreEqual("", actual.TransactionInfo);
        }

        [TestMethod]
        public void Constructor_DataTable()
        {
            // Arrange
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("ErrorCode"));
            dt.Columns.Add(new DataColumn("ErrorMessage"));
            dt.Columns.Add(new DataColumn("TransactionInfo"));
            var row = dt.NewRow();
            row[0] = 0;
            row[1] = "Test Message";
            row[2] = "Test info about the transaction";
            dt.Rows.Add(row);

            // Act
            var actual = new TransactionStatus(dt);

            // Assert
            Assert.AreEqual(TransactionStatusCode.Successful, actual.StatusCode);
            Assert.AreEqual("Test Message", actual.ErrorMessage);
            Assert.AreEqual("Test info about the transaction", actual.TransactionInfo);
        }

        [TestMethod]
        [ExpectedException(typeof(ExecuteTransactionFormatException))]
        public void Constructor_DataTableWithBadColumns()
        {
            var test = new TransactionStatus(new DataTable());
            Assert.IsNotNull(test);
        }

        [TestMethod]
        [ExpectedException(typeof(ExecuteTransactionFormatException))]
        public void Constructor_DataTableWithColumnsButBadRows()
        {
            // Arrange
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("ErrorCode"));
            dt.Columns.Add(new DataColumn("ErrorMessage"));
            dt.Columns.Add(new DataColumn("TransactionInfo"));

            var test = new TransactionStatus(dt);
            Assert.IsNotNull(test);
        }
    }
}
